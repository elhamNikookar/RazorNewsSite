using AutoMapper;
using Blazor.Business.Repository.IRepository;
using Blazor.Data.Context;
using Blazor.Data.Entities.NewsEntities;
using Blazor.Model.DTOs.Newses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Business.Repository
{
    public class NewsRepository : INewsRepository
    {
        #region Constructor
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public NewsRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        #endregion

        public async Task<NewsDTO> CreateNews(NewsDTO newsDTO)
        {
            var news = _mapper.Map<NewsDTO, News>(newsDTO);
            news.CreatedBy = "";
            news.CreateDate = DateTime.Now;
            await _context.Newes.AddAsync(news);
            await _context.SaveChangesAsync();
            return _mapper.Map<News, NewsDTO>(news);
        }

        public Task<IEnumerable<NewsDTO>> GetAllNewses()
        {
            throw new NotImplementedException();
        }

        public Task<NewsDTO> GetNewsById(int newsId)
        {
            throw new NotImplementedException();
        }

        public Task<NewsDTO> IsNewsExistsByTitle(string title)
        {
            throw new NotImplementedException();
        }

        public Task<NewsDTO> RemoveNews(int newsId)
        {
            throw new NotImplementedException();
        }

        public Task<NewsDTO> RemoveNews(NewsDTO newsDTO)
        {
            throw new NotImplementedException();
        }

        public Task<NewsDTO> UpdateNews(int newsId, NewsDTO newsDTO)
        {
            throw new NotImplementedException();
        }
    }
}
