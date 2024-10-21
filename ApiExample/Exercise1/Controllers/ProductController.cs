using Exercise1.Context;
using Exercise1.Models;
using Exercise1.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Exercise1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly DatabaseContext _context;
        private readonly ProductService _productService;

        public ProductController(DatabaseContext context, ProductService productService, ILogger<ProductController> logger)
        {
            _context = context;
            _productService = productService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductModel>>> GetProducts()
        {
            try
            {
                var products = await _context.Products.ToListAsync();
                return products.Select(p => _productService.ConvertToModel(p)).ToList();
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException?.Message);
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductModel>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return _productService.ConvertToModel(product);
        }

        [HttpPost]
        public async Task<ActionResult<ProductModel>> PostProduct(ProductModel productModel)
        {
            var product = _productService.ConvertToEntity(productModel);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, _productService.ConvertToModel(product));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, ProductModel productModel)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            product.Name = productModel.Name;
            product.Price = productModel.Price;

            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
