using Blazor.Business.Repository.IRepository;
using Blazor.Model.DTOs.Common;
using Blazor.Model.DTOs.Newses;
using Microsoft.AspNetCore.Mvc.RazorPages; // برای استفاده از PersianCalendarViewModel

namespace RazorNews.Pages
{
    public class IndexModel : PageModel
    {
        #region Constructor
        public IEnumerable<NewsDTO> NewsesList { get; set; }
        private readonly ILogger<IndexModel> _logger;
        private readonly INewsRepository _newsRepository;

        public IndexModel(ILogger<IndexModel> logger, INewsRepository newsRepository)
        {
            _logger = logger;
            _newsRepository = newsRepository;
        }
        #endregion

        public async Task OnGetAsync()
        {
            NewsesList = await _newsRepository.GetAllNewsesByCount(8);
        }
    }
}
