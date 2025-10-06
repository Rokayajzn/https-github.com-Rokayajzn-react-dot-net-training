using System.ComponentModel.DataAnnotations;

namespace ProductManagementDashboard.Dtos
{
  
        public class UserCreateDto
        {
            [Required, MaxLength(100)]
            public string Username { get; set; }

            [Required, MinLength(6)]
            public string Password { get; set; }

            [Required]
            public int RoleId { get; set; }  
        }
    }


