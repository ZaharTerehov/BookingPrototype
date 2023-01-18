using Booking.ApplicationCore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Booking.ApplicationCore.QueryOptions
{
    public sealed class QueryEntityOptions<TEntity> :BaseQueryOptions<TEntity> where TEntity : class
    {
        private IList<Expression<Func<TEntity, object>>> _includeOptions;
        public IList<Expression<Func<TEntity, object>>> IncludeOptions { get { return _includeOptions; } }

        public QueryEntityOptions<TEntity> AddSortOption(bool descending, Expression<Func<TEntity, object>> sortOption)
        {
            _sortOptions = _sortOptions ?? new List<SortOption<TEntity>>();
            _sortOptions.Add(new SortOption<TEntity>() {Descending = descending, Expression = sortOption });            
            return this;
        }

        public QueryEntityOptions<TEntity> SetFilterOption(Expression<Func<TEntity, bool>> filterOption)
        {
            _filterOption = filterOption;          
            return this;
        }
        public QueryEntityOptions<TEntity> AddIncludeOption(Expression<Func<TEntity, object>> includeOption)
        {
            _includeOptions = _includeOptions ?? new List<Expression<Func<TEntity, object>>>();
            _includeOptions.Add(includeOption);
            return this;
        }

        public QueryEntityOptions<TEntity> SetCurentPageAndPageSize(int currentPage, PageSize pageSize)
        {
            PageSize = pageSize;
            if (currentPage > 0)
            {
                CurrentPage = currentPage;
            }
            return this;
        }

        public QueryEntityOptions<TEntity> NextPage()
        {
            CurrentPage++;
            return this;
        }

        public QueryEntityOptions<TEntity> PrevPage()
        {
            if (CurrentPage > 1)
            {
                CurrentPage--;
            }
            return this;
        }
    }
}
