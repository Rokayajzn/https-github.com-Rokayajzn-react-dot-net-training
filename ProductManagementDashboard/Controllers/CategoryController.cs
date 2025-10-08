using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductManagementDashboard.Repository;
using ProductManagementDashboard.Dtos;
using DataLayer.Models;

namespace ProductManagementDashboard.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _repo;

        public CategoriesController(ICategoryRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var list = await _repo.GetCategoriesWithProductsAsync();
            return Ok(list);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id, CancellationToken ct)
        {
            var cat = await _repo.GetCategoryByIdAsync(id);
            return cat is null ? NotFound(new { message = "Category not found" }) : Ok(cat);
        }

        [HttpPost] // POST /api/categories
        public async Task<IActionResult> Create([FromBody] CategoryCreateDto dto, CancellationToken ct)
        {
            if (dto is null) return BadRequest(new { message = "Body is required" });
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var category = new Category { Name = dto.Name };
            await _repo.AddCategoryAsync(category, ct);

            return CreatedAtAction(nameof(GetById), new { id = category.Id },
                new { message = "Category created successfully", id = category.Id });
        }
    }
}
