using Blazor.Business.Repository;
using Blazor.Business.Repository.IRepository;
using Blazor.Model.DTOs.Newses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorNews.Pages.Newses
{
    public class SearchNewsesModel : PageModel
    {
        #region Constructor

        [BindProperty(SupportsGet = true)]
        public string? SearchKey { get; set; }

        public FilterNewsesDTO Filter { get; set; } = new FilterNewsesDTO();
        private readonly INewsRepository _newsRepository;
        public SearchNewsesModel(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }
        #endregion
        public async Task OnGetAsync()
        {
            //if (string.IsNullOrEmpty(SearchKey))
            //{
            //    Filter.Newses = (List<NewsDTO>)await _newsRepository.GetAllNewses();
            //    Filter.Tittle = "";
            //}
            //Filter = new FilterNewsesDTO();

            Filter.Title = SearchKey;
            Filter = await _newsRepository.FilterNewses(Filter);
        }
    }
}
