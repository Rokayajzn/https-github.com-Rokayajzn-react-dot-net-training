using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Username { get; set; }



        [Required]
        public string PasswordHash { get; set; }
        [Required, MaxLength(50)]
        public string Role { get; set; } = "User"; 
    }


}
