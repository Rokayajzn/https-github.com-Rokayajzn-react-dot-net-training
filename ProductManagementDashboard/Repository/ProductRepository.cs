using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataLayer;


namespace ProductManagementDashboard.Repository
{
    public class ProductRepository : BaseRepository<Product>
    {
        private readonly DbModel _context;

        public ProductRepository(DbModel context) : base(context)
        {
        
        }

        // Get all products with categories
        public async Task<IEnumerable<Product>> GetProductsWithCategoriesAsync()
        {
            return await _context.Products
                .Include(p => p.ProductCategories)
                .ThenInclude(pc => pc.Category)
                .ToListAsync();
        }

        // Get product by Id
        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _context.Products
                .Include(p => p.ProductCategories)
                .ThenInclude(pc => pc.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        // Add new product
        public async Task AddProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        // Update entire product
        public async Task UpdateProductAsync(Product product)
        {
            var existingProduct = await _context.Products.FindAsync(product.Id);
            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Description = product.Description;
                existingProduct.Price = product.Price;
                existingProduct.StockQuantity = product.StockQuantity;
                existingProduct.ImageUrl = product.ImageUrl;

                _context.Products.Update(existingProduct);
                await _context.SaveChangesAsync();
            }
        }

        // Update only stock quantity
        public async Task UpdateStockAsync(int productId, int newStock)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product != null)
            {
                product.StockQuantity = newStock;
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
            }
        }

        // Delete product
        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
