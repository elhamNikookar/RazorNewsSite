using Blazor.Data.Entities.NewsEntities;
using CommonLayer;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Blazor.Model.DTOs.Newses
{
    public class NewsDTO
    {
        public int NewsId { get; set; }

        [Display(Name = "عنوان")]
        [MaxLength(300, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string Title { get; set; }

        [Display(Name = "توضیحات کوتاه")]
        [MaxLength(500, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string ShortDescription { get; set; }

        [Display(Name = "توضیحات اصلی")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string Description { get; set; }

        [Display(Name = "نام تصویر")]
        [MaxLength(300, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string ImageName { get; set; } = StaticDetail.Default_News_Image;

        [Display(Name = "تصویر")]
        public IFormFile? ImageItem { get; set; }

        [Display(Name = "کلمات کلیدی")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(300, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string Tags { get; set; }

        [Display(Name = "تاریخ ثبت")]
        public DateTime CreateDate { get; set; } = DateTime.Now;

        public string? CreatedBy { get; set; }

        [Display(Name = "لیست نظرات")]
        public List<Comment> CommentsNews { get; set; } = new List<Comment>();
        public int ViewCount { get; set; } = 0;


    }
}
