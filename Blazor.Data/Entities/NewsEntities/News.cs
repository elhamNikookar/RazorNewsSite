using System.ComponentModel.DataAnnotations;


namespace Blazor.Data.Entities.NewsEntities
{
    public class News
    {
        [Key]
        public int NewsId { get; set; }

        [MaxLength(300)]
        [Required]
        public string Title { get; set; }

        [MaxLength(500)]
        [Required]
        public string ShortDescription { get; set; }

        [Required]
        public string Description { get; set; }


        [MaxLength(300)]
        [Required]
        public string ImageName { get; set; }

        public DateTime CreateDate { get; set; }

        public bool IsActive { get; set; }

        public string CreatedBy { get; set; }

        public string EditedBy { get; set; }
    }
}
