using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDotNetDemo.Models;
using MongoDotNetDemo.Services.Category;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MongoDotNetDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        // GET: api/Category 
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var categories = await _categoryService.GetAllAsyc();
            return Ok(categories);
        }

        // GET api/CategoryController/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var category = await _categoryService.GetById(id);
            if(category == null)            
                return NotFound();
            
            return Ok(category);
        }

        // POST api/CategoryController
        [HttpPost]
        public async Task<IActionResult> Post(Category category)
        {
            await _categoryService.CreateAsync(category);
            return Ok("Created Succesfully");
        }

        // PUT api/CategoryController/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Category category)
        {
            var checkCategory = _categoryService.GetById(id);
            if(checkCategory == null)
                return NotFound("Category Not Found");

            await _categoryService.UpdateAsync(id, category);
            return Ok("Updated Succesfully");
        }

        // DELETE api/CategoryController/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var checkCategory = _categoryService.GetById(id);
            if (checkCategory == null)
                return NotFound("Category Not Found");

            await _categoryService.DeleteAsync(id);
            return Ok("Deleted Succesfully");
        }
    }
}
