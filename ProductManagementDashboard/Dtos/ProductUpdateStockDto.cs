using System.ComponentModel.DataAnnotations;

namespace ProductManagementDashboard.Dtos
{
    public class ProductUpdateStockDto
    {
        [Required]
        [Range(0, int.MaxValue)]
        public int StockQuantity { get; set; }
    }
}
