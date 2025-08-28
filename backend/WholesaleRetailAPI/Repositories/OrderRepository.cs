using WholesaleRetailAPI.Models;

namespace WholesaleRetailAPI.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;
        public OrderRepository(AppDbContext context) => _context = context;

        public async Task<Order> CreateOrderAsync(Order order, IEnumerable<OrderItem> items)
        {
            // Attach order
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // Add items
            foreach (var item in items)
            {
                item.OrderId = order.OrderId;
                _context.OrderItems.Add(item);

                // Decrement stock
                var product = await _context.Products.FindAsync(item.ProductId);
                if (product != null && product.StockQuantity >= item.Quantity)
                {
                    product.StockQuantity -= item.Quantity;
                }
                else
                {
                    throw new Exception($"Insufficient stock for ProductId {item.ProductId}");
                }
            }

            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Order?> GetByIdAsync(int id) =>
            await _context.Orders.FindAsync(id);
    }
}
