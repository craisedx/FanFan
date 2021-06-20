using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FanFan.Models
{
    public class AppUser : IdentityUser
    {

        [Required(ErrorMessage = "Не указано имя")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Не указан пароль")]
        public string Password { get; set; }
        
        [Required]
        public byte Condition { get; set; }

        public string PhotoUser { get; set; }
        public int? RoleId { get; set; }
        public Role Role { get; set; }
        public AppUser()
        {

        }
    }
}
