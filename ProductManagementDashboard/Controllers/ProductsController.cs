using DataLayer.Models;
using DataLayer.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using User_Mangment_System.Dtos;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ProductRepository _productRepo;

    public ProductsController(ProductRepository productRepo)
    {
        _productRepo = productRepo;
    }

    // GET: api/products
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _productRepo.GetProductsWithCategoriesAsync();
        return Ok(products);
    }

    // GET: api/products/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _productRepo.GetProductByIdAsync(id);
        if (product == null)
            return NotFound(new { message = "Product not found" });

        return Ok(product);
    }

    // POST: api/products
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProductCreateDTO dto)
    {
        var product = new Product
        {
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            StockQuantity = dto.StockQuantity,
            ImageUrl = dto.ImageUrl
        };

        await _productRepo.AddProductAsync(product);
        return Ok(new { message = "Product created successfully" });
    }

    // PUT: api/products/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] ProductUpdateDTO dto)
    {
        var product = await _productRepo.GetProductByIdAsync(id);
        if (product == null)
            return NotFound(new { message = "Product not found" });

        // Map DTO to entity
        product.Name = dto.Name;
        product.Description = dto.Description;
        product.Price = dto.Price;
        product.StockQuantity = dto.StockQuantity;
        product.ImageUrl = dto.ImageUrl;

        await _productRepo.UpdateProductAsync(product);
        return Ok(new { message = "Product updated successfully" });
    }
    [HttpPatch("{id}/update-stock")]
    public async Task<IActionResult> UpdateStock(int id, [FromBody] ProductUpdateDTO dto)
    {
        var product = await _productRepo.GetProductByIdAsync(id);
        if (product == null)
            return NotFound(new { message = "Product not found" });

        await _productRepo.UpdateStockAsync(id, dto.StockQuantity);

        return Ok(new { message = "Stock updated successfully" });
    }




// DELETE: api/products/{id}
[HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _productRepo.DeleteProductAsync(id);
        if (!deleted)
            return NotFound(new { message = "Product not found" });

        return Ok(new { message = "Product deleted successfully" });
    }
}

