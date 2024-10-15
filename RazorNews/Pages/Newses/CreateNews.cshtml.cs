using Blazor.Business.Repository.IRepository;
using Blazor.Model.DTOs.Newses;
using CommonLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace RazorNews.Pages.Newses
{
    [Authorize(Roles = StaticDetail.AdminUser)]

    public class CreateNewsModel : PageModel
    {
        #region Constructor

        [BindProperty]
        public List<IFormFile> Uploads { get; set; }

        [BindProperty]
        public NewsDTO NewsDto { get; set; }

        private readonly INewsRepository _newsRepository;

        public CreateNewsModel(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        #endregion
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (await _newsRepository.IsNewsExistsByTitle(NewsDto.Title))
            {
                ModelState.AddModelError("Tittle", "خبری با این عنوان درسایت ثبت شده است.");
                return Page();
            }

            await _newsRepository.CreateNews(NewsDto);

            return Redirect("/");
        }

        public JsonResult OnPostUploadImage()
        {
            var uploadedFiles = new List<string>(); // لیستی برای ذخیره URL های تصاویر

            if (Uploads != null && Uploads.Count > 0)
            {
                foreach (var upload in Uploads)
                {
                    if (upload.Length > 0)
                    {
                        var fileName = Path.GetFileName(upload.FileName);
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), StaticDetail.Default_Src_News_Image, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            upload.CopyTo(stream);
                        }
                        var url = $"{Request.Scheme}://{Request.Host}/{StaticDetail.Default_Src_News_Image}/{fileName}";
                        uploadedFiles.Add(url); // URL تصویر آپلود شده را ذخیره کنید
                    }
                }
                return new JsonResult(new { uploaded = true, urls = uploadedFiles });
            }
            return new JsonResult(new { uploaded = false });
        }
    }
}
