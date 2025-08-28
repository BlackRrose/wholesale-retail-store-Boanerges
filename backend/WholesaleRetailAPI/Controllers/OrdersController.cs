using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WholesaleRetailAPI.Models;
using WholesaleRetailAPI.Repositories;
using WholesaleRetailAPI.Services;

namespace WholesaleRetailAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepo;
        private readonly ICustomerRepository _customerRepo;
        private readonly IProductRepository _productRepo;
        private readonly PricingService _pricingService;

        /// <summary>
        /// Constructor injecting repositories
        /// </summary>
        public OrdersController(IOrderRepository orderRepo, ICustomerRepository customerRepo,
                           IProductRepository productRepo, PricingService pricingService)
        {
            _orderRepo = orderRepo;
            _customerRepo = customerRepo;
            _productRepo = productRepo;
            _pricingService = pricingService;
        }

        public record CreateOrderRequest(int CustomerId, List<OrderItemRequest> Items);
        public record OrderItemRequest(int ProductId, int Quantity);


        /// <summary>
        /// Creates a new order and decrements stock
        /// </summary>
        /// <param name="request">Order request containing customer and items</param>
        /// <returns>Created Order object</returns>
        [HttpPost("createOrder")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {
            // Add try and catch

            var customer = await _customerRepo.GetByIdAsync(request.CustomerId);
            if (customer == null) return NotFound("Customer not found");

            var order = new Order
            {
                CustomerId = request.CustomerId,
                OrderDate = DateTime.UtcNow,
                TotalAmount = 0m
            };

            var orderItems = new List<OrderItem>();
            decimal orderTotal = 0m;

            // Calculate total price and check stock
            foreach (var itemReq in request.Items)
            {
                var product = await _productRepo.GetByIdAsync(itemReq.ProductId);
                if (product == null) return NotFound($"Product {itemReq.ProductId} not found");
                if (product.StockQuantity < itemReq.Quantity) return BadRequest($"Insufficient stock for {product.Name}");

                // Gets price for specific customer
                var finalPrice = await _pricingService.GetPriceForCustomerAsync(customer, product, itemReq.Quantity);

                // Create order entity
                orderItems.Add(new OrderItem
                {
                    ProductId = product.ProductId,
                    Quantity = itemReq.Quantity,
                    Price = finalPrice
                });

                orderTotal += finalPrice;
            }

            order.TotalAmount = orderTotal;

            var createdOrder = await _orderRepo.CreateOrderAsync(order, orderItems);

            return Ok(createdOrder);
        }

        /// <summary>
        /// Retrieves an order by ID
        /// </summary>
        /// <param name="id">Order ID</param>
        /// <returns>Order object</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _orderRepo.GetByIdAsync(id);
            if (order == null) return NotFound();
            return Ok(order);
        }

        /// <summary>
        /// Returns a quote for an order with pricing rules and discounts applied
        /// </summary>
        /// <param name="request">Order request containing items and customer</param>
        /// <returns>Quote with subtotal, applied discounts, and total</returns>
        [HttpPost("getQuote")]
        public async Task<IActionResult> GetQuote([FromBody] CreateOrderRequest request)
        {
            var quote = await _pricingService.GetQuoteAsync(request);
            return Ok(quote);
        }
    }
}
