using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Api
{
    public class PagedListProvider<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }

        private bool _hasPrevious => PageIndex > 1;
        public bool HasPrevious { get { return _hasPrevious; } private set { value = _hasPrevious; } }


        private bool _hasNext => PageIndex < TotalPages;
        public bool HasNext { get { return _hasNext; } private set { value = _hasNext; } }


        public PagedListProvider(List<T> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            PageIndex = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            AddRange(items);
        }

        public static PagedListProvider<T> ToPagedList(List<T> source, int pageIndex, int pageSize)
        {
            if (pageIndex == 0)
                pageIndex = 1;
            if (pageSize == 0)
                pageSize = 10;

            var count = source.Count();
            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return new PagedListProvider<T>(items, count, pageIndex, pageSize);
        }

        public static PagedListProvider<T> ToPagedList(IEnumerable<T> source, int pageIndex, int pageSize)
        {
            if (pageIndex == 0)
                pageIndex = 1;
            if (pageSize == 0)
                pageSize = 10;

            var count = source.Count();
            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return new PagedListProvider<T>(items, count, pageIndex, pageSize);
        }

        public static PagedListProvider<T> ToPagedList(IQueryable<T> source, int pageIndex, int pageSize)
        {
            if (pageIndex == 0)
                pageIndex = 1;
            if (pageSize == 0)
                pageSize = 10;

            var count = source.Count();
            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return new PagedListProvider<T>(items, count, pageIndex, pageSize);
        }

        public PagedList GetPaging()
        {
            return new PagedList(PageIndex, TotalPages, PageSize, TotalCount, HasPrevious, HasNext);
        }

    }
}
