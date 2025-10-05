using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    public class Category : BaseModel
    {
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Name { get; set; }

        // Many-to-many
        public ICollection<ProductCategory> ProductCategories { get; set; }

    }
}
