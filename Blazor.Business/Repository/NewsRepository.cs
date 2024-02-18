
using AutoMapper;
using Blazor.Business.Repository.IRepository;
using Blazor.Data.Context;
using Blazor.Data.Entities.NewsEntities;
using Blazor.Model.DTOs.Newses;
using Blazor.Model.DTOs.Paging;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Business.Repository
{
    public class NewsRepository : INewsRepository
    {
        #region constructor

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
            var addedNews = await _context.Newses.AddAsync(news);
            await _context.SaveChangesAsync();
            return _mapper.Map<News, NewsDTO>(addedNews.Entity);
        }

        public async Task<NewsDTO> UpdateNews(int newsId, NewsDTO newsDTO)
        {
            try
            {
                if (newsId == newsDTO.NewsId)
                {
                    News newsDetail = await _context.Newses.FindAsync(newsId);
                    News news = _mapper.Map<NewsDTO, News>(newsDTO, newsDetail);
                    news.EditedBy = "";
                    _context.Newses.Update(news);
                    await _context.SaveChangesAsync();
                    return _mapper.Map<News, NewsDTO>(news);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<NewsDTO> GetNewsById(int newsId)
        {
            try
            {
                NewsDTO news = _mapper.Map<News, NewsDTO>(await _context.Newses.SingleOrDefaultAsync(s => s.NewsId == newsId));
                return news;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<IEnumerable<NewsDTO>> GetAllNewses()
        {
            try
            {
                IEnumerable<NewsDTO> newsDTOs = _mapper.Map<IEnumerable<News>, IEnumerable<NewsDTO>>(await _context.Newses.ToListAsync());
                return newsDTOs;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<IEnumerable<NewsDTO>> GetAllNewsesByCount(int count)
        {
            try
            {
                var data = await _context
                    .Newses
                    .OrderByDescending(s => s.CreateDate)
                    .Take(count)
                    .ToListAsync();
                IEnumerable<NewsDTO> newsDTOs = _mapper.Map<IEnumerable<News>, IEnumerable<NewsDTO>>(data);
                return newsDTOs;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<NewsDTO> IsNewsExistsByTitle(string title, int newsId)
        {
            return _mapper.Map<News, NewsDTO>(await _context.Newses.FirstOrDefaultAsync(s => s.Title == title && s.NewsId != newsId));
        }

        public async Task<int> RemoveNews(int newsId)
        {
            var news = await _context.Newses.FindAsync(newsId);
            if (news != null)
            {
                _context.Newses.Remove(news);
                await _context.SaveChangesAsync();

                return news.NewsId;
            }

            return 0;
        }

        public async Task<int> RemoveNews(NewsDTO news)
        {
            return await RemoveNews(news.NewsId);
        }

        public async Task<FilterNewsesDTO> FilterNewses(FilterNewsesDTO filter)
        {
            var query = _context.Newses.AsQueryable();

            if (!string.IsNullOrEmpty(filter.Title))
            {
                query = query.Where(s => EF.Functions.Like(s.Title, $"%{filter.Title}%"));
            }

            var allEntitiesCount = await query.CountAsync();

            var pager = Pager.Build(filter.Page, filter.Take, allEntitiesCount, filter.HowManyShowAfterBefore);

            var newses = await query.Paging(pager).ToListAsync();

            filter.Newses = _mapper.Map<List<News>, List<NewsDTO>>(newses);

            return filter.SetPaging(pager);
        }
    }
}
