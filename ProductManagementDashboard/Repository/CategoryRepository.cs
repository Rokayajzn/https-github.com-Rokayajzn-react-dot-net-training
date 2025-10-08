using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace ProductManagementDashboard.Repository
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(DataLayer.DbModel context) : base(context) { }

        public async Task<IEnumerable<Category>> GetCategoriesWithProductsAsync()
        {
            return await _context.Categories
                .Include(c => c.ProductCategories)
                .ThenInclude(pc => pc.Product)
                .ToListAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories
                .Include(c => c.ProductCategories)
                .ThenInclude(pc => pc.Product)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddCategoryAsync(Category category, CancellationToken ct = default)
        {
            // Option 2: base methods don't accept ct yet
            await AddAsync(category);
            await SaveChangesAsync();
        }

        public async Task<bool> UpdateCategoryAsync(Category category, CancellationToken ct = default)
        {
            var existing = await _context.Categories.FindAsync(category.Id);
            if (existing is null) return false;

            existing.Name = category.Name;

            _context.Categories.Update(existing);
            await SaveChangesAsync();
            return true;
        }
    }
}
