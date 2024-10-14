using Blazor.Data.Entities.NewsEntities;

namespace Blazor.Model.DTOs.Newses
{
    public class NewsViewModel
    {
        public NewsDTO NewsDto { get; set; }
        public List<Comment> CommentList { get; set; } = new List<Comment>();
        public HashSet<int> LikedComments { get; set; } = new HashSet<int>(); 
        public Comment NewComment { get; set; } = new Comment();
    }

}
