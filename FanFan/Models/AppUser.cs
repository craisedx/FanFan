using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FanFan.Models
{
    public class AppUser : IdentityUser
    {

       
        [Required(ErrorMessage = "Не указан пароль")]
        public string Password { get; set; }
        public string PhotoUser { get; set; }
        [Required]
        public Status UserState { get; set; }
        public bool IsChecked { get; set; }

    }
}
