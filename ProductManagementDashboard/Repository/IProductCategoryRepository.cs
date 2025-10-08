

  using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DataLayer.Models;

namespace ProductManagementDashboard.Repository
    {
        public interface IProductCategoryRepository : IBaseRepository<ProductCategory>
        {
            Task<bool> LinkAsync(int productId, int categoryId, CancellationToken ct = default);
            Task<bool> UnlinkAsync(int productId, int categoryId, CancellationToken ct = default);

            Task<IEnumerable<Category>> GetCategoriesForProductAsync(int productId, CancellationToken ct = default);
            Task<IEnumerable<Product>> GetProductsForCategoryAsync(int categoryId, CancellationToken ct = default);

            Task<int> BulkLinkAsync(int productId, IEnumerable<int> categoryIds, CancellationToken ct = default);
            Task<bool> ExistsAsync(int productId, int categoryId, CancellationToken ct = default);
        }
    }


