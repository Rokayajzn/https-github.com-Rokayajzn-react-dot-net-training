
namespace ProductManagementDashboard.Dtos
{
    public class ProductCategoryLinkDto
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
    }

    public class ProductCategoryBulkLinkDto
    {
        public int ProductId { get; set; }
        public List<int> CategoryIds { get; set; } = new();
    }
}
