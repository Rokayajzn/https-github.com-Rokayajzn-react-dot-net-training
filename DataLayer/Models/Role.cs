using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataLayer.Models
    {
        public class Role
        {
            public int Id { get; set; }

            [Required, MaxLength(50)]
            public string Name { get; set; } = string.Empty;

            // Navigation property (one role can have many users)
            public ICollection<User> Users { get; set; } = new List<User>();
        }
    }

