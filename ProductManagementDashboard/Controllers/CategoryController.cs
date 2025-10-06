using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using ProductManagementDashboard.Repository; // <-- keep this one
using ProductManagementDashboard.Dtos;

namespace ProductManagementDashboard.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryRepository _categoryRepo;

        public CategoryController(CategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryRepo.GetCategoriesWithProductsAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryRepo.GetCategoryByIdAsync(id);
            if (category == null)
                return NotFound(new { message = "Category not found" });

            return Ok(category);
        }

        [HttpPost("add-category")]
        public async Task<IActionResult> Create([FromBody] CategoryCreateDto dto)
        {
            var category = new Category { Name = dto.Name };
            await _categoryRepo.AddCategoryAsync(category);
            return Ok(new { message = "Category created successfully" });
        }
    }
}
