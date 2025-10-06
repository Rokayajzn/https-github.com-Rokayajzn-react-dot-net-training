using System.ComponentModel.DataAnnotations;

namespace ProductManagementDashboard.Dtos
{
    public class UserLoginDto
    {
        [Required, MaxLength(100)]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
