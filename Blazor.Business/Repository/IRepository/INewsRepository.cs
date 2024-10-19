
using Blazor.Data.Entities.NewsEntities;
using Blazor.Model.DTOs.Newses;

namespace Blazor.Business.Repository.IRepository
{
    public interface INewsRepository
    {
        public Task<NewsDTO> CreateNews(NewsDTO newsDTO);
        public Task<NewsDTO> UpdateNews(int newsId, NewsDTO newsDTO);
        public Task<NewsDTO> UpdateNews(NewsDTO newsDTO);
        public Task<NewsDTO> GetNewsById(int newsId);
        public Task<IEnumerable<NewsDTO>> GetAllNewses();
        public Task<IEnumerable<NewsDTO>> GetAllNewsesByCount(int count);
        public Task<bool> IsNewsExistsByTitle(string tittle);
        public Task<NewsDTO> IsNewsExistsByTitle(string tittle, int newsId);
        public Task<int> RemoveNews(int newsId);
        public Task<int> RemoveNews(NewsDTO news);
        public Task<FilterNewsesDTO> FilterNewses(FilterNewsesDTO filter);
        public Task<IEnumerable<TitleAndImageNewsDTO>> GetNewsesImageByCount(int count);

        #region Comment
        public Task<bool> ToggleLike(int commentId, bool isLiked);
        public Task<NewsDTO> CreateCommentForNews(int newsId, CommentDTO comment);
        public Task<NewsDTO> GetNewsByIdWhithComment(int newsId);
        public Task<Comment> GetCommentById(int commentId);
        public Task<IEnumerable<CommentDTO>> GetAllCommentsByDate(int count = 0);
        public Task<IEnumerable<Comment>> GetAllCommentsByLike(int count = 0);
        public Task<bool> RemoveComment(int commentId);
        public Task<bool> UpdateComment(CommentDTO comment);
        #endregion
    }
}