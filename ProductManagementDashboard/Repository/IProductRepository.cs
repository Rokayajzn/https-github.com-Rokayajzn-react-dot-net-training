using DataLayer.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Collections.Generic;

namespace ProductManagementDashboard.Repository
{
    public interface IProductRepository : IBaseRepository<Product>
    {
            
        Task<IEnumerable<Product>> GetProductsWithCategoriesAsync();
        Task<Product?> GetProductByIdAsync(int id);

        Task AddProductAsync(Product product, CancellationToken ct = default);
        Task<bool> UpdateProductAsync(Product product, CancellationToken ct = default);
        Task<bool> UpdateStockAsync(int productId, int newStock, CancellationToken ct = default);
        Task<bool> DeleteProductAsync(int id, CancellationToken ct = default);
    }
}


