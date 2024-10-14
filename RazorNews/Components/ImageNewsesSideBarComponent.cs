using Blazor.Model.DTOs.Newses;
using Microsoft.AspNetCore.Mvc;

namespace RazorNews.Components
{
    public class ImageNewsesSideBarComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(List<NewsDTO> newsesList)
        {
            return View("/Views/Components/ImageNewsesSideBarComponent.cshtml", newsesList);
        }

        public string GetTimeAgo(DateTime dateTime)
        {
            var timeSpan = DateTime.Now - dateTime;

            if (timeSpan.Days > 365)
            {
                int years = (int)(timeSpan.Days / 365);
                return $"{years} سال پیش";
            }
            else if (timeSpan.Days > 30)
            {
                int months = (int)(timeSpan.Days / 30);
                return $"{months} ماه پیش";
            }
            else if (timeSpan.Days > 0)
            {
                return $"{timeSpan.Days} روز پیش";
            }
            else if (timeSpan.Hours > 0)
            {
                return $"{timeSpan.Hours} ساعت پیش";
            }
            else if (timeSpan.Minutes > 0)
            {
                return $"{timeSpan.Minutes} دقیقه پیش";
            }
            else
            {
                return "چند لحظه پیش";
            }
        }
    }
}

