using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Infrastructure.Data.DBExtentions
{
    public static class TEntityListQuery
    {
        public static IQueryable<TEntity> FromSqlQquery<TEntity>(this DbSet<TEntity> entities, string sqlQuery) where TEntity : class
        {
            if (!sqlQuery.IsNullOrEmpty())
            {
                return entities.FromSqlRaw(sqlQuery);                
            }
            return entities;
        }
    }
}
