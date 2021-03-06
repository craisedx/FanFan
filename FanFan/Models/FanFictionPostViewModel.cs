using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FanFan.Models
{
    public class FanFictionPostViewModel
    {
      
        [Required]
        public string AppUserId { get; set; }
       

        [Required]
        public string Name { get; set; }
        [Required]
        public string ShortDescription { get; set; }

        [Required]
        public virtual int FandomId { get; set; }

      
        public IFormFile Picture { get; set; }
    }
}
