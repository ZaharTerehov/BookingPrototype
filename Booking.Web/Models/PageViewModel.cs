using Booking.ApplicationCore.Enums;
using System.Drawing.Printing;

namespace Booking.Web.Models
{
    public class PageViewModel
    {
        public int CurrentPage { get; private set; }
        public PageSize PageSize { get; private set; } 

        public int CurrentElementsCount { get; private set; }   

        public PageViewModel(int count, int currentPage, PageSize pageSize)
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
