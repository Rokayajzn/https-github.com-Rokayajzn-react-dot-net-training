
using System.ComponentModel.DataAnnotations;


namespace DataLayer.Models
{
    public abstract class BaseModel
    {
        [Key]
        public int Id { get; set; }
    }
}
 

