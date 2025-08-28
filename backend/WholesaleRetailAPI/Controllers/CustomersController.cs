    using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WholesaleRetailAPI.Models;
using WholesaleRetailAPI.Repositories;

namespace WholesaleRetailAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _repository;
        public CustomersController(ICustomerRepository repository) => _repository = repository;

        [HttpPost("addCustomer")]
        public async Task<IActionResult> AddCustomer([FromBody] Customer customer)
        {
            // Add try and catch
            if (string.IsNullOrWhiteSpace(customer.Email)) return BadRequest("Email required");

            var created = await _repository.AddAsync(customer);
            return CreatedAtAction(nameof(AddCustomer), new { id = created.CustomerId }, created);
        }
    }
}
