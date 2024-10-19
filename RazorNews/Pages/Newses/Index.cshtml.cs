using Blazor.Business.Repository.IRepository;
using Blazor.Model.DTOs.Newses;
using CommonLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorNews.Pages.Newses
{
    [Authorize]
    public class IndexModel : PageModel
    {
        #region Constructor
        private readonly INewsRepository _newsRepository;
        public List<NewsDTO> NewsList { get; set; }
        public IndexModel(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }
        #endregion

        public async Task OnGetAsync()
        {
            NewsList = (List<NewsDTO>)await _newsRepository.GetAllNewses();
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            _newsRepository.RemoveNews(id);

            return RedirectToPage();
        }
    }
}



//[Authorize(Roles = StaticDetail.AdminUser)]
