using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FanFan.Models
{
    public class ChapterModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string ChapterText { get; set; }
        [Required]
        public IFormFile Picture { get; set; }
        [Required]
        public int FanFictionPostId { get; set; }
        
    }
}
