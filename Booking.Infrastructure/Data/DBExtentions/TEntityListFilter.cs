﻿using Booking.ApplicationCore.QueryOptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Infrastructure.Data.DBExtentions
{
    public static class TEntityListFilter
    {
        public static IQueryable<TEntity> FilterEntities<TEntity>(this IQueryable<TEntity> entities, Expression<Func<TEntity, bool>> filterOptions)
        {
            if (filterOptions != null)
            {
                entities = entities.Where(filterOptions);             
            }
            return entities;
        }
    }
}
