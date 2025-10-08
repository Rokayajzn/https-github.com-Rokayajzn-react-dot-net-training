using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductManagementDashboard.Repository;
using ProductManagementDashboard.Dtos;

namespace ProductManagementDashboard.Controllers
{
    [ApiController]
    [Route("api/product-categories")]
    public class ProductCategoriesController : ControllerBase
    {
        private readonly IProductCategoryRepository _repo;

        public ProductCategoriesController(IProductCategoryRepository repo)
        {
            _repo = repo;
        }

        // GET /api/product-categories/product/5  -> categories for a product
        [HttpGet("product/{productId:int}")]
        public async Task<IActionResult> GetCategoriesForProduct(int productId, CancellationToken ct)
        {
            var list = await _repo.GetCategoriesForProductAsync(productId, ct);
            return Ok(list);
        }

        // GET /api/product-categories/category/7 -> products in a category
        [HttpGet("category/{categoryId:int}")]
        public async Task<IActionResult> GetProductsForCategory(int categoryId, CancellationToken ct)
        {
            var list = await _repo.GetProductsForCategoryAsync(categoryId, ct);
            return Ok(list);
        }

        // POST /api/product-categories  (link)
        [HttpPost]
        public async Task<IActionResult> Link(ProductCategoryLinkDto dto, CancellationToken ct)
        {
            if (dto is null) return BadRequest(new { message = "Body is required" });

            var created = await _repo.LinkAsync(dto.ProductId, dto.CategoryId, ct);
            if (!created)
                return Conflict(new { message = "Link already exists or could not be created" });

            // route to check: GET categories for product
            return CreatedAtAction(nameof(GetCategoriesForProduct),
                new { productId = dto.ProductId },
                new { message = "Linked", productId = dto.ProductId, categoryId = dto.CategoryId });
        }

        // POST /api/product-categories/bulk
        [HttpPost("bulk")]
        public async Task<IActionResult> BulkLink(ProductCategoryBulkLinkDto dto, CancellationToken ct)
        {
            if (dto is null || dto.CategoryIds is null)
                return BadRequest(new { message = "Invalid body" });

            var added = await _repo.BulkLinkAsync(dto.ProductId, dto.CategoryIds, ct);
            return Ok(new { message = "Bulk link complete", added });
        }

        // DELETE /api/product-categories/5/7 (unlink)
        [HttpDelete("{productId:int}/{categoryId:int}")]
        public async Task<IActionResult> Unlink(int productId, int categoryId, CancellationToken ct)
        {
            var removed = await _repo.UnlinkAsync(productId, categoryId, ct);
            if (!removed) return NotFound(new { message = "Link not found" });
            return NoContent();
        }
    }
}

