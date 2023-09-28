using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDotNetDemo.Models;
using MongoDotNetDemo.Services.Product;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MongoDotNetDemo.Controllers
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
        // GET: api/product 
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _productService.GetAllAsyc();
            return Ok(products);
        }

        // GET api/productController/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var product = await _productService.GetById(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        // POST api/productController
        [HttpPost]
        public async Task<IActionResult> Post(Product product)
        {
            product.CategoryName = null;
            await _productService.CreateAsync(product);
            return Ok("Created Succesfully");
        }

        // PUT api/productController/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Product product)
        {
            product.CategoryName = null;
            var checkproduct = _productService.GetById(id);
            if (checkproduct == null)
                return NotFound("product Not Found");

            await _productService.UpdateAsync(id, product);
            return Ok("Updated Succesfully");
        }

        // DELETE api/productController/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var checkproduct = _productService.GetById(id);
            if (checkproduct == null)
                return NotFound("product Not Found");

            await _productService.DeleteAsync(id);
            return Ok("Deleted Succesfully");
        }
    }
}
