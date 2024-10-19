using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blazor.Data.Entities.NewsEntities
{
    public class Comment
    {
        public Comment()
        {

        }

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

        public DateTime CreateDate { get; set; }
        public int Like { get; set; } = 0;
        public int Dislike { get; set; } = 0;
        public bool IsActive { get; set; } = false;

        [Display(Name = "پاسخ به نظر")]
        public string? AnswerComment { get; set; }

        #region Relations
        public int NewsId { get; set; }
        [ForeignKey("NewsId")]
        public News News { get; set; }


        #endregion

    }
}
