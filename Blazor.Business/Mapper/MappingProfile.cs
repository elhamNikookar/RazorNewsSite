using AutoMapper;
using Blazor.Data.Entities.NewsEntities;
using Blazor.Model.DTOs.Newses;


namespace Blazor.Business.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<NewsDTO, News>();
            CreateMap<News, NewsDTO>();

            CreateMap<Comment, CommentDTO>();
            CreateMap<CommentDTO, Comment>();
        }
    }
}
