using System.ComponentModel.DataAnnotations;

namespace ProductManagementDashboard.Dtos;

public class ProductUpdateDTO
{
    [Required, MaxLength(200)]
    public string Name { get; set; }

    [Required, MaxLength(200)]
    public string Description { get; set; }

    public int StockQuantity { get; set; }

    [Required]
    public decimal Price { get; set; }

    public string ImageUrl { get; set; }
}
