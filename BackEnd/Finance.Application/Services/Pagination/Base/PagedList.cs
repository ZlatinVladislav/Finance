using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Finance.Application.Services.Pagination.Base
{
    public class PagedList<T> : List<T>
    {
        public PagedList(IEnumerable<T> items, int currentPage, int totalPages, int pageSize, int totalCount)
        {
            CurrentPage = currentPage;
            TotalPages = totalPages;
            PageSize = pageSize;
            TotalCount = totalCount;
            AddRange(items);
        }

        private PagedList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
        {
            CurrentPage = pageNumber;
            TotalPages = (int) Math.Ceiling(count / (double) pageSize);
            PageSize = pageSize;
            TotalCount = count;
            AddRange(items);
        }

        public int CurrentPage { get; }
        public int TotalPages { get; }
        public int PageSize { get; }
        public int TotalCount { get; }

        public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}