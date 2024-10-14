using Blazor.Model.DTOs.Common;
using Microsoft.AspNetCore.Mvc;

namespace RazorNews.Controllers
{
    public class CalendarController : Controller
    {
        public IActionResult PersianCalendar()
        {
            var viewModel = new PersianCalendarViewModel();
            return PartialView("PersianCalendarPartial", viewModel);
        }
    }

}
