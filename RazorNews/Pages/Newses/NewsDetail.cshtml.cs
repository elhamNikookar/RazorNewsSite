using Blazor.Business.Repository.IRepository;
using Blazor.Data.Entities.NewsEntities;
using Blazor.Model.DTOs.Common;
using Blazor.Model.DTOs.Newses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Web.Mvc;

namespace RazorNews.Pages.Newses
{
    public class NewsDetailModel : PageModel
    {
        [BindProperty]
        public CommentDTO NewComment { get; set; }
        [BindProperty]
        public int NewsID { get; set; } 
        
        public NewsDTO NewsDto { get; set; }
        public IEnumerable<NewsDTO> NewsesListSidebar { get; set; }

        private readonly INewsRepository _newsRepository;
        public PersianCalendarViewModel PersianCalendar { get; set; }
        public NewsDetailModel(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
            PersianCalendar = new PersianCalendarViewModel(); // مقداردهی اولیه
        }

        public async Task OnGetAsync(int id)
        {
            NewsesListSidebar = await _newsRepository.GetAllNewsesByCount(4);
            NewsDto = await _newsRepository.GetNewsByIdWhithComment(id);
            ViewData["KeyWords"] = NewsDto.Tags.Replace(" ","");
            if (NewsDto != null)
            {
                NewsID = NewsDto.NewsId;
                NewsDto.ViewCount += 1;
                await _newsRepository.UpdateNews(id, NewsDto);
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await _newsRepository.CreateCommentForNews(NewsID, NewComment);
            return Redirect("/");
        }

    }
}


