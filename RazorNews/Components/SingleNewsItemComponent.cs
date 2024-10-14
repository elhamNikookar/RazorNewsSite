using Blazor.Model.DTOs.Newses;
using Microsoft.AspNetCore.Mvc;

namespace RazorNews.Components
{
    public class SingleNewsItemComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(NewsDTO newsItem)
        {
            return View("/Views/Components/SingleNewsItemComponent.cshtml", newsItem);
        }
    }
}
