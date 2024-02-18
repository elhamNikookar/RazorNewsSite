using System.Collections.Generic;
using Blazor.Model.DTOs.Paging;

namespace Blazor.Model.DTOs.Newses
{
    public class FilterNewsesDTO : BasePaging
    {
        public string Title { get; set; }

        public List<NewsDTO> Newses { get; set; }

        public FilterNewsesDTO SetPaging(BasePaging paging)
        {
            Page = paging.Page;
            Take = paging.Take;
            Skip = paging.Skip;
            AllEntitiesCount = paging.AllEntitiesCount;
            AllPageCount = paging.AllPageCount;
            StartPage = paging.StartPage;
            EndPage = paging.EndPage;
            HowManyShowAfterBefore = paging.HowManyShowAfterBefore;

            return this;
        }
    }
}
