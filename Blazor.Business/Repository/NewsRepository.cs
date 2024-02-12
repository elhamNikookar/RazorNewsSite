using AutoMapper;
using Blazor.Business.Repository.IRepository;
using Blazor.Data.Context;
using Blazor.Data.Entities.NewsEntities;
using Blazor.Model.DTOs.Newses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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
            news.CreatedBy = "no body";
            news.EditedBy = "no body";
            news.CreateDate = DateTime.Now;
            await _context.Newes.AddAsync(news);
            await _context.SaveChangesAsync();
            return _mapper.Map<News, NewsDTO>(news);
        }

        public async Task<IEnumerable<NewsDTO>> GetAllNewses()
        {
            try
            {
                IEnumerable<NewsDTO> newsDTOs = _mapper.Map<IEnumerable<News>, IEnumerable<NewsDTO>>(await _context.Newes.ToListAsync());
                return newsDTOs;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<NewsDTO> GetNewsById(int newsId)
        {
            try
            {
                return _mapper.Map<News, NewsDTO>(await _context.Newes.FindAsync(newsId));
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<NewsDTO> IsNewsExistsByTitle(string title, int newsId)
        {
            try
            {
                return _mapper.Map<News, NewsDTO>(await _context.Newes.SingleOrDefaultAsync(n => n.Title == title && n.NewsId != newsId));
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<int> RemoveNews(int newsId)
        {
            var news = await _context.Newes.FindAsync(newsId);
            if (news != null)
            {
                _context.Newes.Remove(news);
                await _context.SaveChangesAsync();
                return news.NewsId;
            }
            return 0;
        }

        public async Task<int> RemoveNews(NewsDTO newsDTO)
        {
            return await RemoveNews(newsDTO.NewsId);
        }

        public async Task<NewsDTO> UpdateNews(int newsId, NewsDTO newsDTO)
        {
            try
            {
                News currenNews = await _context.Newes.FindAsync(newsId);

                if (currenNews == null) return null;
                if (currenNews.NewsId != newsDTO.NewsId) return null;

                News upadateNews = _mapper.Map<NewsDTO, News>(newsDTO, currenNews);
                upadateNews.EditedBy = "";

                _context.Newes.Update(upadateNews);
                await _context.SaveChangesAsync();
                return _mapper.Map<News, NewsDTO>(upadateNews);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
