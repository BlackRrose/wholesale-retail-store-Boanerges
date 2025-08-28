using WholesaleRetailAPI.Models;

namespace WholesaleRetailAPI.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context) => _context = context;

        public async Task<Customer> AddAsync(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer?> GetByIdAsync(int id) =>
            await _context.Customers.FindAsync(id);
    }
}
