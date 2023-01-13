using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Booking.ApplicationCore.QueryOptions
{
    public sealed class QueryOptions<TEntity>
    {
        private IList<SortOption<TEntity>> _sortOptions;
        public IList<SortOption<TEntity>> SortOptions { get { return _sortOptions; } }

        private Expression<Func<TEntity, bool>> _filterOption;
        public Expression<Func<TEntity, bool>> FilterOption { get { return _filterOption; } }

        private IList<Expression<Func<TEntity, object>>> _includeOptions;
        public IList<Expression<Func<TEntity, object>>> IncludeOptions { get { return _includeOptions; } }

        public QueryOptions<TEntity> AddSortOption(bool descending, Expression<Func<TEntity, object>> sortOption)
        {
            _sortOptions = _sortOptions ?? new List<SortOption<TEntity>>();
            _sortOptions.Add(new SortOption<TEntity>() {Descending = descending, Expression = sortOption });            
            return this;
        }

        public QueryOptions<TEntity> SetFilterOption(Expression<Func<TEntity, bool>> filterOption)
        {
            _filterOption = filterOption;          
            return this;
        }
        public QueryOptions<TEntity> AddIncludeOption(Expression<Func<TEntity, object>> includeOption)
        {
            _includeOptions = _includeOptions ?? new List<Expression<Func<TEntity, object>>>();
            _includeOptions.Add(includeOption);
            return this;
        }
    }
}
