using Blazor.Model.DTOs.Newses;

namespace Blazor.Business.Repository.IRepository
{
    public interface INewsRepository
    {
        public Task<NewsDTO> CreateNews(NewsDTO newsDTO);
        public Task<NewsDTO> UpdateNews(int newsId, NewsDTO newsDTO);
        public Task<NewsDTO> GetNewsById(int newsId);
        public Task<IEnumerable<NewsDTO>> GetAllNewses();
        public Task<NewsDTO> IsNewsExistsByTitle(string title , int newsId);
        public Task<int> RemoveNews(int newsId);
        public Task<int> RemoveNews(NewsDTO newsDTO);
    }
}