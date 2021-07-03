using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FanFan.Models
{
    public class FanFictionPost
    {
        public int Id { get; set; }
        [Required]
        public  string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string ShortDescription { get; set; }

        [Required]
        public virtual int FandomId { get; set; }
        public Fandom Fandom { get; set; }

        [Required]
        public string Picture { get; set; }
    }
}
