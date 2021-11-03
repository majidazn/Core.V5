using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Api
{
    public class PagedList
    {

        public int PageIndex { get; }
        public int TotalPages { get; }
        public int PageSize { get; }
        public int TotalCount { get; }
        public bool HasPrevious { get; }
        public bool HasNext { get; }


        public PagedList(int pageIndex, int totalPages, int pageSize, int totalCount, bool hasPrevious, bool hasNext)
        {
            this.PageIndex = pageIndex;
            this.TotalPages = totalPages;
            this.PageSize = pageSize;
            this.TotalCount = totalCount;
            this.HasPrevious = hasPrevious;
            this.HasNext = hasNext;
        }

    }
}
