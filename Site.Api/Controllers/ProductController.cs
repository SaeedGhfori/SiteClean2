using Microsoft.AspNetCore.Mvc;
using Site.Application.Definitions.Contracts.Interfaces.Products;
using Site.Application.Definitions.Models.Products;

namespace Site.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            var products = await _productService.GetAllAsync();
            if (products == null || !products.Any())
            {
                return NotFound("No products found.");
            }
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new ArgumentNullException());
            }

            var product = await _productService.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound($"Product with ID {id} not found.");
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            if (product == null || product.Price == null)
            {
                return BadRequest(new ArgumentNullException());
            }

            var createdProduct = await _productService.CreateAsync(product);
            return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.Id }, createdProduct);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> UpdateProduct(int id, [FromBody] Product product)
        {
            if (id <= 0 || product == null || product.Price == null)
            {
                return BadRequest(new ArgumentNullException());
            }

            var updatedProduct = await _productService.UpdateAsync(id, product);
            return Ok(updatedProduct);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new ArgumentNullException());
            }
            await _productService.DeleteAsync(id);
            return NoContent();
        }
    }
}
