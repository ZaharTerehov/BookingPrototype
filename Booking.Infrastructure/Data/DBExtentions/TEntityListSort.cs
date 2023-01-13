using Booking.ApplicationCore.QueryOptions;
using Microsoft.AspNetCore.Server.IIS.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Infrastructure.Data.DBExtentions
{
    public static class TEntityListSort
    {
        public static IQueryable<TEntity> OrderEntityBy<TEntity>(this IQueryable<TEntity> entities, IList<SortOption<TEntity>> orderByOptions)
        {
            if (orderByOptions != null)
            {
                var firstOption = orderByOptions.First();
                IOrderedQueryable<TEntity> query = firstOption.Descending ? 
                                                                            entities.OrderByDescending(firstOption.Expression): 
                                                                            entities.OrderBy(firstOption.Expression);
                foreach (var item in orderByOptions.Skip(1))
                {
                    query = item.Descending ? 
                                            query.ThenByDescending(item.Expression): 
                                            query.ThenBy(item.Expression);
                }                
                return query;
            }
            return entities;
        }
    }
}
