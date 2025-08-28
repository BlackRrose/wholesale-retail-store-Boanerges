using Microsoft.EntityFrameworkCore;
using WholesaleRetailAPI.Models;

namespace WholesaleRetailAPI.Repositories
{
    public class ProductRepository : IProductRepository
    {

        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context) => _context = context;


        public async Task<IEnumerable<Product>> GetAllAsync() =>
        await _context.Products.ToListAsync();

        public async Task<Product?> GetByIdAsync(int id) =>
            await _context.Products.FindAsync(id);

        public async Task<Product> AddAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }
    }
}
