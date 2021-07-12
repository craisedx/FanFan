using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FanFan.Models
{
    public class Comment
    {
        public int Id { get; set; }
        [Required]
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        [Required]
        public int FanFictionPostId { get; set; }
        public FanFictionPost FanFictionPost { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
    }
}
