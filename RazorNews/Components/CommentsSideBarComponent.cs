using Blazor.Business.Repository.IRepository;
using Blazor.Model.DTOs.Newses;
using Microsoft.AspNetCore.Mvc;

namespace RazorNews.Components
{
    public class CommentsSideBarComponent : ViewComponent
    {
        #region Constructor
        private readonly INewsRepository _newsRepository;

        public CommentsSideBarComponent(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }
        #endregion
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("/Views/Components/CommentsSideBarComponent.cshtml",await _newsRepository.GetAllCommentsByLike());
        }
    }
}
