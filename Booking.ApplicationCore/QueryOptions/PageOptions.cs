using Booking.ApplicationCore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.ApplicationCore.QueryOptions
{
    public class PageOptions
    {
        public int CurrentPage { get; set; }
        public PageSize PageSize { get; set; }

        public int CurrentElementsCount { get; set; }

        public PageOptions(int count, int currentPage, PageSize pageSize)
        {
            CurrentPage = currentPage;
            CurrentElementsCount = count;
            PageSize = pageSize;
        }

        public bool HasPreviousPage
        {
            get
            {
                return (CurrentPage > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                var ps = (int)PageSize;
                return (CurrentElementsCount == ps);
            }
        }
    }
}
