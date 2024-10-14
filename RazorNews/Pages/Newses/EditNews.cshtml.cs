using Blazor.Business.Repository.IRepository;
using Blazor.Model.DTOs.Newses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorNews.Pages.Newses
{
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class EditNewsModel : PageModel
    {

        #region Constructor

        [BindProperty]
        public NewsDTO NewsDto { get; set; } = default;

        private readonly INewsRepository _newsRepository;

        public EditNewsModel(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        #endregion

        public async Task OnGetAsync(int id)
        {
            NewsDto = await _newsRepository.GetNewsById(id);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            await _newsRepository.UpdateNews(NewsDto);

            return Redirect("/newseslist");
        }
    }
}
