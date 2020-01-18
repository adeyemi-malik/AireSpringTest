using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AireSpringTest.Core.Paging
{
    public class PaginatedList<T>
    {
        public PaginatedList(IQueryable<T> source, int count)
        {
            TotalCount = count;
            Rows = source;
        }

        public int TotalCount { get; }

        public IEnumerable<T> Rows { get; }
    }

    public class PaginationMeta
    {
        public PaginationMeta()
        {

        }
        public PaginationMeta(int currentPage, int currentPageSize, int totalPages, int totalRecords)
        {
            CurrentPage = currentPage;
            CurrentPageSize = currentPageSize;
            TotalPages = totalPages;
            TotalRecords = totalRecords;
        }

        public int CurrentPage { get; }

        public int CurrentPageSize { get; }

        public int TotalPages { get; }

        public int TotalRecords { get; }

    }
}
