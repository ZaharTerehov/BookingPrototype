using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Infrastructure.Data.DBExtentions
{
    public static class TEntityListSelect
    {
        public static IQueryable<TVM> SelectEntities<TEntity, TVM>(this IQueryable<TEntity> entities, Expression<Func<TEntity, TVM>> selectOptions)
        {
            if (selectOptions != null)
            {
                return entities.Select(selectOptions);
            }
            throw new Exception("Not filled SelectOption");
        }
    }
}
