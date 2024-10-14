using Blazor.Business.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;


namespace RazorNews.Components
{
    public class NewsSliderComponent : ViewComponent
    {
        private readonly INewsRepository _newsRepository;

        public NewsSliderComponent(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync(int count = 8)
        {
            var newsList = await _newsRepository.GetAllNewsesByCount(count); // گرفتن لیست اخبار
            ViewBag.CurrentSlide = 0; // شروع اسلایدر از اولین اسلاید
            return View("/Views/Components/NewsSliderComponent.cshtml",newsList); // ارسال داده‌ها به View مربوط به این View Component
        }
    }
}
