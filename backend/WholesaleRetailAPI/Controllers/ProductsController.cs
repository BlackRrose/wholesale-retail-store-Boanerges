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

        /// <summary>
        /// Constructor injecting the Product Repository
        /// </summary>
        /// <param name="repository">Repository for managing products</param
        public ProductsController(IProductRepository repository) => _repository = repository;


        /// <summary>
        /// Retrieves the list of all products
        /// </summary>
        /// <returns>List of Product objects</returns>
        [HttpGet("getProducts")]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _repository.GetAllAsync();
            return Ok(products);
        }

        /// <summary>
        /// Creates a new product in the system
        /// </summary>
        /// <param name="product">Product object to create</param>
        /// <returns>Created Product object</returns>
        [HttpPost("addProduct")]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {
            // Add try and catch
            if (string.IsNullOrWhiteSpace(product.Name)) return BadRequest("Product name required");
            if (product.Price <= 0) return BadRequest("Price must be > 0");

            // Save product using repository
            var created = await _repository.AddAsync(product);

            // Return 201 Created response with product
            return CreatedAtAction(nameof(AddProduct), new { id = created.ProductId }, created);
        }
    }
}
