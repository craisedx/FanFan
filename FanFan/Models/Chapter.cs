using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FanFan.Models
{
    public class Chapter
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string ChapterText { get; set; }
        [Required]
        public string Picture { get; set; }
        [Required]
        public int FanFictionPostId { get; set; }
        public FanFictionPost FanFictionPost { get; set; }
    }
}
