namespace Booking.Web.Models
{
    public class PageViewModel
    {
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; } 

        public int CurrentElementsCount { get; private set; }   

        public PageViewModel(int count, int currentPage, int pageSize)
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
                return (CurrentElementsCount == PageSize);
            }
        }
    }
}
