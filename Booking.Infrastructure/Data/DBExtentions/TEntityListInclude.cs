using Booking.ApplicationCore.QueryOptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Infrastructure.Data.DBExtentions
{
    public static class TEntityListInclude
    {
        public static IQueryable<TEntity> IncludeFields<TEntity>(this IQueryable<TEntity> entities, IList<Expression<Func<TEntity, object>>> includeOptions) where TEntity : class 
        {
            if (includeOptions != null && includeOptions.Count > 0)
            {
                var firstOption = includeOptions.First();
                var query = entities.Include(firstOption);
                foreach (var item in includeOptions.Skip(1))
                {
                    query = query.Include(item);
                }
                return query;
            }
            return entities;
        }
    }
}
