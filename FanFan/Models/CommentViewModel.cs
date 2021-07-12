using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FanFan.Models
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        [Required]
        public string AppUserId { get; set; }
        [Required]
        public int FanFictionPostId { get; set; }
        public DateTime Date { get; set; }
        [Required(ErrorMessage = ("Поле не может остаться пустым"))]
        public string Text { get; set; }
    }
}
