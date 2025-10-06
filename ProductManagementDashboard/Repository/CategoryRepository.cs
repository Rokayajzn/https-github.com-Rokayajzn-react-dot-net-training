
using DataLayer.Models;
using ProductManagementDashboard.Repository;
using Microsoft.EntityFrameworkCore;


namespace ProductManagementDashboard.Repository
{
    public class CategoryRepository: BaseRepository<Category>

    {
        private readonly DataLayer.DbModel _context;
    
 

        public CategoryRepository(DataLayer.DbModel context) : base(context)
        {
            
        }
        // Get all categories with their products
        public async Task<IEnumerable<Category>> GetCategoriesWithProductsAsync()
        {
            return await _context.Categories
                .Include(c => c.ProductCategories)
                .ThenInclude(pc => pc.Product)
                .ToListAsync();
        }
        // Get category by Id
        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories
                .Include(c => c.ProductCategories)
                .ThenInclude(pc => pc.Product)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
        // Add new category
        public async Task AddCategoryAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }
        // Update entire category
        public async Task UpdateCategoryAsync(Category category)
        {
            var existingCategory = await _context.Categories.FindAsync(category.Id);
            if (existingCategory != null)
            {
                existingCategory.Name = category.Name;
               
                _context.Categories.Update(existingCategory);
                await _context.SaveChangesAsync();
            }
        }
    }
}
