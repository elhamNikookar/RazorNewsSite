using Blazor.Model.DTOs.Newses;

namespace Blazor.Business.Repository.IRepository
{
    public interface INewsRepository
    {
        public Task<NewsDTO> CreateNews(NewsDTO newsDTO);
        public Task<NewsDTO> UpdateNews(int newsId, NewsDTO newsDTO);
        public Task<NewsDTO> GetNewsById(int newsId);
        public Task<IEnumerable<NewsDTO>> GetAllNewses();
        public Task<NewsDTO> IsNewsExistsByTitle(string title);
        public Task<NewsDTO> RemoveNews(int newsId);
        public Task<NewsDTO> RemoveNews(NewsDTO newsDTO);
    }
}