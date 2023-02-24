using Booking.ApplicationCore.Constants;
using Booking.ApplicationCore.Enums;
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
        protected Expression<Func<TEntity, bool>> _filterOption;

        public IList<SortOption<TEntity>> SortOptions { get { return _sortOptions; } }        
        public Expression<Func<TEntity, bool>> FilterOption { get { return _filterOption; } }
        public PageOptions PageOptions { get; set; } = new PageOptions(1, 1, PageSize.AllElements);
    }
}
