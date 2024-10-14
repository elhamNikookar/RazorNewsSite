using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace Blazor.Data.Entities.NewsEntities
{
    public class News
    {
        public News()
        {

        }

        [Key]
        public int NewsId { get; set; }

        [MaxLength(300)]
        [Required]
        public string Title { get; set; }

        [MaxLength(500)]
        [Required]
        public string ShortDescription { get; set; }

        [Required]
        [AllowHtml]
        public string Description { get; set; }


        [MaxLength(300)]
        [Required]
        public string? ImageName { get; set; }

        public DateTime CreateDate { get; set; }

        public bool IsActive { get; set; } = true;
        public int ViewCount { get; set; } = 0;

        [MaxLength(300)]
        public string Tags { get; set; }

        [MaxLength(100)]
        public string? CreatedBy { get; set; }

        [MaxLength(100)]
        public string? EditedBy { get; set; }

        public bool IsDeleted { get; set; } = false;

        #region Relations
        public List<Comment> CommentsNews { get; set; } = new List<Comment>();
        #endregion
    }

}
