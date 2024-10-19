using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Model.DTOs.Newses
{
    public class CommentDTO
    {
        [Key]
        public int CommentId { get; set; }

        [MaxLength(100)]
        [Display(Name = "نام شما")]
        public string CreatedBy { get; set; }

        [MaxLength(100)]
        [Display(Name = "ایمیل شما")]
        public string MailCreator { get; set; }

        [Display(Name = "نظر شما")]
        public string DescriptionComment { get; set; }

        public DateTime? CreateDate { get; set; }

        [Display(Name = "پاسخ به نظر")]
        public string? AnswerComment { get; set; }
        public bool IsActive { get; set; } = false;
        public int? NewsId { get; set; }

        public int Like { get; set; } = 0;
        public int Dislike { get; set; } = 0;

        

    }
}
