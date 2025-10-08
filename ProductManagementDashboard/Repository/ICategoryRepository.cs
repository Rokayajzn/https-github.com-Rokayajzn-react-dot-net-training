using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using DataLayer.Models;

namespace ProductManagementDashboard.Repository
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Task<IEnumerable<Category>> GetCategoriesWithProductsAsync();
        Task<Category?> GetCategoryByIdAsync(int id);

        // Uses BaseRepository<T>.AddAsync + SaveChangesAsync (no ct passed to base for now)
        Task AddCategoryAsync(Category category, CancellationToken ct = default);

        // Returns true if updated, false if not found
        Task<bool> UpdateCategoryAsync(Category category, CancellationToken ct = default);
    }
}


