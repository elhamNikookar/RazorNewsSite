using Blazor.Business.Repository.IRepository;
using Blazor.Model.DTOs.Newses;
using Microsoft.AspNetCore.Mvc;

namespace RazorNews.Components
{
    public class CommentsListNewsComponent : ViewComponent
    {
        private readonly INewsRepository _newsRepository;

        public CommentsListNewsComponent(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        // متد برای فراخوانی و دریافت اطلاعات نظرات
        public async Task<IViewComponentResult> InvokeAsync(int newsId)
        {
            var news = await _newsRepository.GetNewsById(newsId);
            var viewModel = new NewsViewModel
            {
                NewsDto = news,
                CommentList = news.CommentsNews,
                LikedComments = new HashSet<int>() // می‌توانید اطلاعات نظرات لایک شده را از دیتابیس یا کوکی‌ها پر کنید
            };

            return View("/Views/Components/CommentsListNewsComponent.cshtml", viewModel);
        }

        // متد برای ثبت نظر جدید
        //public async Task<IActionResult> AddComment(int newsId, NewsViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var newComment = model.NewComment;
        //        var result = await _newsRepository.CreateCommentForNews(newsId, newComment);

        //        if (result != null)
        //        {
        //            return RedirectToAction("Details", new { newsId = newsId });
        //        }
        //        ModelState.AddModelError("", "خطا در ثبت نظر");
        //    }

        //    return ViewComponent("NewsComments", new { newsId = newsId });
        //}

        // متد برای لایک کردن نظر
        //public async Task<IActionResult> LikeComment(int commentId, int newsId)
        //{
        //    bool isLiked = false; // اینجا باید بررسی شود که کاربر قبلاً لایک کرده یا نه
        //    var result = await _newsRepository.ToggleLike(commentId, isLiked);

        //    if (result)
        //    {
        //        // بازگشت به کامپوننت
        //        return ViewComponent("NewsComments", new { newsId = newsId });
        //    }

        //    return ViewComponent("NewsComments", new { newsId = newsId });
        //}
    }
}
