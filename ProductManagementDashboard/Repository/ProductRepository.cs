using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;


namespace ProductManagementDashboard.Repository
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(DbModel context) : base(context) { }

        // Read with includes
        public async Task<IEnumerable<Product>> GetProductsWithCategoriesAsync()
        {
            return await _context.Products
                .Include(p => p.ProductCategories)
                .ThenInclude(pc => pc.Category)
                .ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _context.Products
                .Include(p => p.ProductCategories)
                .ThenInclude(pc => pc.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        // Create (uses BaseRepository<T> under the hood)
        public async Task AddProductAsync(Product product, CancellationToken ct = default)
        {
            await AddAsync(product);      // base method (no ct yet)
            await SaveChangesAsync();     // base method
        }

        // Full update
        public async Task<bool> UpdateProductAsync(Product product, CancellationToken ct = default)
        {
            var existing = await _context.Products.FindAsync(product.Id);
            if (existing is null) return false;

            existing.Name = product.Name;
            existing.Description = product.Description;
            existing.Price = product.Price;
            existing.StockQuantity = product.StockQuantity;
            existing.ImageUrl = product.ImageUrl;

            _context.Products.Update(existing);
            await SaveChangesAsync();
            return true;
        }

        // Partial update (stock only)
        public async Task<bool> UpdateStockAsync(int productId, int newStock, CancellationToken ct = default)
        {
            var existing = await _context.Products.FindAsync(productId);
            if (existing is null) return false;

            existing.StockQuantity = newStock;
            _context.Products.Update(existing);
            await SaveChangesAsync();
            return true;
        }

        // Delete
        public async Task<bool> DeleteProductAsync(int id, CancellationToken ct = default)
        {
            var existing = await _context.Products.FindAsync(id);
            if (existing is null) return false;

            _context.Products.Remove(existing);
            await SaveChangesAsync();
            return true;
        }
    }
}
