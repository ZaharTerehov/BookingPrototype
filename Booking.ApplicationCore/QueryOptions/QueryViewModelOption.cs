using Booking.ApplicationCore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Booking.ApplicationCore.QueryOptions
{
    public sealed class QueryViewModelOption<TEntity, TVM> :BaseQueryOptions<TEntity> where TEntity : class //where TVM : class 
    {
        private Expression<Func<TEntity, TVM>> _selectOption;
        public Expression<Func<TEntity, TVM>> SelectOption { get { return _selectOption; } }

        public QueryViewModelOption<TEntity, TVM> AddSortOption(bool descending, Expression<Func<TEntity, object>> sortOption)
        {
            _sortOptions = _sortOptions ?? new List<SortOption<TEntity>>();
            _sortOptions.Add(new SortOption<TEntity>() { Descending = descending, Expression = sortOption });
            return this;
        }

        public QueryViewModelOption<TEntity, TVM> SetFilterOption(Expression<Func<TEntity, bool>> filterOption)
        {
            _filterOption = filterOption;
            return this;
        }
        public QueryViewModelOption<TEntity, TVM> AddSelectOption(Expression<Func<TEntity, TVM>> selectOption)
        {
            _selectOption = selectOption;
            return this;
        }

        public QueryViewModelOption<TEntity, TVM> SetCurentPageAndPageSize(PageOptions options)
        {
            PageOptions.PageSize = options.PageSize;
            if (options.CurrentPage > 0)
            {
                PageOptions.CurrentPage = options.CurrentPage;
            }
            return this;
        }

        public QueryViewModelOption<TEntity, TVM> NextPage()
        {
            PageOptions.CurrentPage++;
            return this;
        }

        public QueryViewModelOption<TEntity, TVM> PrevPage()
        {
            if (PageOptions.CurrentPage > 1)
            {
                PageOptions.CurrentPage--;
            }
            return this;
        }
    }
}
