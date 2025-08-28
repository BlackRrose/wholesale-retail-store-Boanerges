using WholesaleRetailAPI.Models;

namespace WholesaleRetailAPI.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();

        Task<Product?> GetByIdAsync(int id);

        Task<Product> AddAsync(Product product);
    }
}
