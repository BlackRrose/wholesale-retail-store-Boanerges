using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WholesaleRetailAPI.Models;
using WholesaleRetailAPI.Repositories;

namespace WholesaleRetailAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repository;
        public ProductsController(IProductRepository repository) => _repository = repository;


        [HttpGet("getProducts")]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _repository.GetAllAsync();
            return Ok(products);
        }

        [HttpPost("addProduct")]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {
            // Add try and catch
            if (string.IsNullOrWhiteSpace(product.Name)) return BadRequest("Product name required");
            if (product.Price <= 0) return BadRequest("Price must be > 0");

            var created = await _repository.AddAsync(product);
            return CreatedAtAction(nameof(AddProduct), new { id = created.ProductId }, created);
        }
    }
}
