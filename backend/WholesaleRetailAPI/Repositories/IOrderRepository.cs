using WholesaleRetailAPI.Models;

namespace WholesaleRetailAPI.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> CreateOrderAsync(Order order, IEnumerable<OrderItem> items);

        Task<Order?> GetByIdAsync(int id);
    }
}
