using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FanFan.Models
{
    public class PostAndChapter
    {
        [Required(ErrorMessage = "Не указано название фанфика")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Длина имени должна быть от 3 до 30 символов")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Не указано описание")]
        public string ShortDescription { get; set; }

        [Required]
        public virtual int FandomId { get; set; }
        

        [Required(ErrorMessage = "Не задана картинка")]
        public string Picture { get; set; }
        [Required(ErrorMessage = "Не задана картинка для главы")]
        public string ChapterPicture { get; set; }
        [Required(ErrorMessage = "Не указано название главы")]
        public string ChapterName { get; set; }
        [Required(ErrorMessage = "Не заполнено содержимое главы")]
        public string ChapterText { get; set; }
        [Required]
        public int FanFictionPostId { get; set; }

    }
}
