using Booking.ApplicationCore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Infrastructure.Data.DBExtentions
{
    public static class TEntityListSkipTake
    {
        public static IQueryable<TEntity> SkipTakeEntities<TEntity>(this IQueryable<TEntity> entities, int currentPage, PageSize pageSize)
        {
            int ps = (int) pageSize;
            return pageSize switch
            {
                PageSize.AllElements => entities,
                _ => entities.Skip((currentPage - 1) * ps)
                             .Take(ps)
            };
           
        }
    }
}
