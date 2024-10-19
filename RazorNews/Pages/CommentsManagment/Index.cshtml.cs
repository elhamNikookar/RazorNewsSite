using Blazor.Business.Repository.IRepository;
using Blazor.Model.DTOs.Newses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorNews.Pages.CommentsManagment
{
    [Authorize]
    public class IndexModel : PageModel
    {
        #region Constructor
        private readonly INewsRepository _newsRepository;
      
        [BindProperty]
        public IList<CommentDTO> Comments { get; set; }

        public IndexModel(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }
        #endregion


        public async Task OnGetAsync()
        {
            Comments = (IList<CommentDTO>)await _newsRepository.GetAllCommentsByDate();
        }

        public async Task<IActionResult> OnPostUpdate(CommentDTO commentDto)
        {
            if (!ModelState.IsValid)
            {
                Comments = (IList<CommentDTO>)await _newsRepository.GetAllCommentsByDate();
                return Page();
            }

            await _newsRepository.UpdateComment(commentDto);
            return RedirectToPage();
        }
    }
}
