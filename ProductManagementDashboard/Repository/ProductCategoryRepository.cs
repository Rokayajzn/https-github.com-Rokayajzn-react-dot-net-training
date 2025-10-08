using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
namespace ProductManagementDashboard.Repository
{
    public class ProductCategoryRepository : BaseRepository<ProductCategory>, IProductCategoryRepository
    {

    
        public ProductCategoryRepository(DbModel context) : base(context) { }

        public async Task<bool> ExistsAsync(int productId, int categoryId, CancellationToken ct = default)
        {
            return await _context.ProductCategory
                .AnyAsync(pc => pc.ProductId == productId && pc.CategoryId == categoryId);
        }

        public async Task<bool> LinkAsync(int productId, int categoryId, CancellationToken ct = default)
        {
            // avoid duplicates
            if (await ExistsAsync(productId, categoryId)) return false;

            var link = new ProductCategory { ProductId = productId, CategoryId = categoryId };

            // Option 2 (no ct to base yet)
            await AddAsync(link);
            await SaveChangesAsync();
            return true;
        }

        public async Task<bool> UnlinkAsync(int productId, int categoryId, CancellationToken ct = default)
        {
            var link = await _context.ProductCategory.FindAsync(productId, categoryId);
            if (link is null) return false;

            _context.ProductCategory.Remove(link);
            await SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Category>> GetCategoriesForProductAsync(int productId, CancellationToken ct = default)
        {
            return await _context.ProductCategory
                .Where(pc => pc.ProductId == productId)
                .Select(pc => pc.Category)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsForCategoryAsync(int categoryId, CancellationToken ct = default)
        {
            return await _context.ProductCategory
                .Where(pc => pc.CategoryId == categoryId)
                .Select(pc => pc.Product)
                .ToListAsync();
        }

        public async Task<int> BulkLinkAsync(int productId, IEnumerable<int> categoryIds, CancellationToken ct = default)
        {
            var ids = categoryIds?.Distinct().ToList() ?? new List<int>();
            if (ids.Count == 0) return 0;

            // fetch existing links to skip duplicates
            var existingIds = await _context.ProductCategory
                .Where(pc => pc.ProductId == productId && ids.Contains(pc.CategoryId))
                .Select(pc => pc.CategoryId)
                .ToListAsync();

            var toInsert = ids.Except(existingIds)
                              .Select(catId => new ProductCategory { ProductId = productId, CategoryId = catId })
                              .ToList();

            if (toInsert.Count == 0) return 0;

            await _context.ProductCategory.AddRangeAsync(toInsert);
            await SaveChangesAsync();
            return toInsert.Count;
        }
    }
}
