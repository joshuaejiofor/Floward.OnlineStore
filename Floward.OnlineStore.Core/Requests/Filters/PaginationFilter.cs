using System;

namespace Floward.OnlineStore.Core.Requests.Filters
{
    public class PaginationFilter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public PaginationFilter()
        {
            PageNumber = 1;
            PageSize = 100;
        }
        public PaginationFilter(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = (pageSize < 0 || pageSize > 1000) ? 1000 : pageSize;
        }

        public PaginationFilter(int pageNumber, int pageSize, DateTime? fromDate, DateTime? toDate)
            : this(pageNumber, pageSize)
        {
            FromDate = fromDate;
            ToDate = toDate;
        }

    }
}
