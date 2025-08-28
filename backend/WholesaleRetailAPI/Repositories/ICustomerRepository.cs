using WholesaleRetailAPI.Models;

namespace WholesaleRetailAPI.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer> AddAsync(Customer customer);

        Task<Customer?> GetByIdAsync(int id);
    }
}
