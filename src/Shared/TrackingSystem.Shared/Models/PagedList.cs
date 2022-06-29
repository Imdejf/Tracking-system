using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackingSystem.Shared.Models
{
    public sealed class PagedList<TElement>
    {
        public IList<TElement> Items { get; private set; }
        public int PageNumber { get; init; }
        public int TotalPages { get; init; }
        public int TotalCount { get; init; }
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;

        private PagedList(IList<TElement> items, int count, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalCount = count;
            Items = items;
        }
        private PagedList()
        {

        }
        public static PagedList<TElement> Create(IQueryable<TElement> source, int pageNumber, int pageSize)
        {
            if (pageNumber < 1) throw new ArgumentException("PageNumber must be 1 or more");
            if (pageSize < 1) throw new ArgumentException("PageSize must be 1 or more");

            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new PagedList<TElement>(items, count, pageNumber, pageSize);
        }

        public static PagedList<TElement> Create(IEnumerable<TElement> source, int pageNumber, int pageSize, int totalCount)
        {
            if (pageNumber < 1) throw new ArgumentException("PageNumber must be 1 or more");
            if (pageSize < 1) throw new ArgumentException("PageSize must be 1 or more");

            return new PagedList<TElement>(source.ToList(), totalCount, pageNumber, pageSize);
        }

        public PagedList<TCast> CastItems<TCast>(Func<TElement, TCast> castFunction)
        {
            return new PagedList<TCast>
            {
                Items = this.Items.Select(castFunction).ToList(),
                PageNumber = this.PageNumber,
                TotalCount = this.TotalCount,
                TotalPages = this.TotalPages
            };
        }
    }
}
