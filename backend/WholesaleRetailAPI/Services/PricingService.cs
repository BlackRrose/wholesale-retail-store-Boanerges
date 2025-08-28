using Microsoft.EntityFrameworkCore;
using WholesaleRetailAPI.Models;
using static WholesaleRetailAPI.Controllers.OrdersController;

namespace WholesaleRetailAPI.Services
{
    public class PricingService
    {
        private readonly AppDbContext _context;

        public PricingService(AppDbContext context) => _context = context;

        public async Task<decimal> GetPriceForCustomerAsync(Customer customer, Product product, int quantity)
        {
            decimal basePrice = product.Price * quantity;

            // Find applicable pricing rule
            var rule = await _context.PricingRules
                .Where(r => r.CustomerType == customer.CustomerType && r.Active && quantity >= r.MinQuantity)
                .OrderByDescending(r => r.MinQuantity) // apply best matching rule
                .FirstOrDefaultAsync();

            if (rule != null)
            {
                var discount = basePrice * (rule.DiscountPercentage / 100m);
                return basePrice - discount;
            }

            return basePrice; // no discount
        }

        public async Task<QuoteResponse> GetQuoteAsync(CreateOrderRequest request)
        {
            var customer = await _context.Customers.FindAsync(request.CustomerId);
            if (customer == null)
                throw new Exception("Customer not found");

            var response = new QuoteResponse
            {
                CustomerId = request.CustomerId
            };

            decimal subtotal = 0;

            foreach (var item in request.Items)
            {
                var product = await _context.Products.FindAsync(item.ProductId);
                if (product == null)
                    continue;

                //Base unit price
                decimal unitPrice = product.Price;

                // Determine price based on customer type
                if (customer.CustomerType == "Wholesale")
                {
                    unitPrice *= 0.9m; // example: 10% off wholesale
                    if (item.Quantity >= 10)
                    {
                        unitPrice *= 0.95m; // extra 5% off for bulk
                        response.DiscountsApplied.Add($"Bulk discount applied for {product.Name}");
                    }
                }

                decimal lineTotal = unitPrice * item.Quantity;
                subtotal += lineTotal;

                response.Items.Add(new QuoteItem
                {
                    ProductId = product.ProductId,
                    ProductName = product.Name,
                    Quantity = item.Quantity,
                    UnitPrice = unitPrice,
                    Total = lineTotal
                });
            }

            response.Subtotal = subtotal;

            // Example: retail seasonal promo
            if (customer.CustomerType == "Retail" && subtotal > 100)
            {
                response.DiscountsApplied.Add("Retail promo: 5% off orders above $100");
                subtotal *= 0.95m;
            }

            response.FinalTotal = subtotal;
            return response;
        }
    }
}
