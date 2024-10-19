using AutoMapper;
using Blazor.Business.Repository.IRepository;
using Blazor.Data.Context;
using Blazor.Data.Entities.NewsEntities;
using Blazor.Model.DTOs.Newses;
using Blazor.Model.DTOs.Paging;
using CommonLayer;
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
            news.CreateDate = DateTime.Now;

            #region Save Image

            if (newsDTO.ImageItem != null)
            {
                string imagePath = "";
                news.ImageName = NameGenerator.GeneratorUniqCode() + Path.GetExtension(newsDTO.ImageItem.FileName);
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/" + StaticDetail.Default_Src_News_Image, news.ImageName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    newsDTO.ImageItem.CopyTo(stream);
                }
            }
            else
                news.ImageName = StaticDetail.Default_News_Image;
            #endregion


            var addedNews = await _context.Newses.AddAsync(news);
            await _context.SaveChangesAsync();
            return _mapper.Map<News, NewsDTO>(addedNews.Entity);
        }

        public async Task<NewsDTO> UpdateNews(NewsDTO newsDTO)
        {
            try
            {
                News newsDetail = await _context.Newses.FindAsync(newsDTO.NewsId);
                if (newsDetail != null)
                {
                    News news = _mapper.Map<NewsDTO, News>(newsDTO, newsDetail);
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

        public async Task<NewsDTO> UpdateNews(int newsId, NewsDTO newsDTO)
        {
            try
            {
                if (newsId == newsDTO.NewsId)
                {
                    News newsDetail = await _context.Newses.FindAsync(newsId);
                    News news = _mapper.Map<NewsDTO, News>(newsDTO, newsDetail);
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
                IEnumerable<NewsDTO> newsDTOs = _mapper.Map<IEnumerable<News>, IEnumerable<NewsDTO>>
                    (await _context.Newses.OrderByDescending(s => s.CreateDate)
                    .ToListAsync());

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
        public async Task<IEnumerable<TitleAndImageNewsDTO>> GetNewsesImageByCount(int count)
        {
            try
            {
                IEnumerable<TitleAndImageNewsDTO> data = await _context.Newses
                    .OrderByDescending(s => s.CreateDate)
                    .Select(n => new TitleAndImageNewsDTO()
                    {
                        NewsId = n.NewsId,
                        Title = n.Title,
                        CreateDate = n.CreateDate,
                        ImageName = n.ImageName
                    })
                    .Take(count)
                    .ToListAsync();
                ++count;
                return data;
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

        public async Task<bool> IsNewsExistsByTitle(string title)
        {
            return await _context.Newses.AnyAsync(s => s.Title == title);
        }

        public async Task<int> RemoveNews(int newsId)
        {
            var news = await _context.Newses.FindAsync(newsId);
            if (news != null)
            {
                news.IsDeleted = true;
                _context.Newses.Update(news);
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

        public async Task<bool> ToggleLike(int commentId, bool isLiked)
        {
            var comment = await _context.Comments.FindAsync(commentId);
            if (comment == null) return false;

            if (isLiked) comment.Like -= 1;
            else comment.Like += 1;

            _context.Comments.Update(comment);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<CommentDTO>> GetAllCommentsByDate(int count = 0)
        {
            try
            {
                if (count > 0)
                    return _mapper.Map<IEnumerable<Comment>, IEnumerable<CommentDTO>>(await _context.Comments.OrderByDescending(s => s.CreateDate).Take(count).ToListAsync());
                else
                    return _mapper.Map<IEnumerable<Comment>, IEnumerable<CommentDTO>>(await _context.Comments.OrderByDescending(s => s.CreateDate).ToListAsync());

            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Comment>> GetAllCommentsByLike(int count = 0)
        {
            try
            {
                if (count > 0)
                    return await _context.Comments.OrderByDescending(s => s.Like).Take(count).ToListAsync();
                else
                    return await _context.Comments.OrderByDescending(s => s.Like).ToListAsync();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public async Task<NewsDTO> CreateCommentForNews(int newsId, CommentDTO comment)
        {
            try
            {
                Comment newComment = new Comment();
                newComment.CreateDate = DateTime.Now;
                newComment.NewsId = newsId;
                newComment.DescriptionComment = comment.DescriptionComment;
                newComment.CreatedBy = comment.CreatedBy;
                newComment.MailCreator = comment.MailCreator;
                newComment.NewsId = newsId;
                await _context.Comments.AddAsync(newComment);
                News news = await _context.Newses.Include(c => c.CommentsNews)
                    .FirstOrDefaultAsync(c => c.NewsId == newsId);
                news.CommentsNews.Add(newComment);

                await _context.SaveChangesAsync();
                return _mapper.Map<News, NewsDTO>(news);
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public async Task<Comment> GetCommentById(int commentId)
        {
            try
            {
                return await _context.Comments.FirstOrDefaultAsync(c => c.CommentId == commentId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<NewsDTO> GetNewsByIdWhithComment(int newsId)
        {
            try
            {
                NewsDTO news = _mapper.Map<News, NewsDTO>(await _context.Newses
                    .Include(c => c.CommentsNews)
                    .SingleOrDefaultAsync(s => s.NewsId == newsId));
                return news;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public async Task<bool> RemoveComment(int commentId)
        {
            Comment comment = await GetCommentById(commentId);
            NewsDTO newsDto = await GetNewsByIdWhithComment(commentId);
            newsDto.CommentsNews.Remove(comment);
            comment.IsActive = false;
            UpdateNews(newsDto.NewsId, newsDto);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateComment(CommentDTO commentDto)
        {
            if (commentDto == null) return false;
            Comment CurrenComment = await _context.Comments.FindAsync(commentDto.CommentId);
            if (CurrenComment == null) return false;

            CurrenComment.AnswerComment = commentDto.AnswerComment;
            CurrenComment.IsActive = commentDto.IsActive;
            CurrenComment.DescriptionComment = commentDto.DescriptionComment;
            CurrenComment.CreatedBy = commentDto.CreatedBy;
            _context.Comments.Update(CurrenComment);
            _context.SaveChanges();
            return true;

            //try
            //{
            //    Comment commentDetail = await _context.Comments.FindAsync(commentDto.CommentId);
            //    if (commentDetail != null)
            //    {
            //        Comment comment = _mapper.Map<CommentDTO, Comment>(commentDto, commentDetail);
            //        _context.Comments.Update(comment);
            //        await _context.SaveChangesAsync();
            //        return _mapper.Map<Comment, CommentDTO>(comment);
            //    }
            //    else
            //    {
            //        return null;
            //    }
            //}
            //catch (Exception e)
            //{
            //    return null;
            //}
        }
    }
}
