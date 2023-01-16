using Booking.ApplicationCore.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Booking.ApplicationCore.QueryOptions
{
    public abstract class BaseQueryOptions<TEntity> where TEntity : class
    {
        protected IList<SortOption<TEntity>> _sortOptions;
        public IList<SortOption<TEntity>> SortOptions { get { return _sortOptions; } }

        protected Expression<Func<TEntity, bool>> _filterOption;
        public Expression<Func<TEntity, bool>> FilterOption { get { return _filterOption; } }

        public int PageSize { get; set; } = ApplicationConstants.ApartmentsPageSize;

        public int CurrentPage { get; protected set; } = 1;


    }
}
