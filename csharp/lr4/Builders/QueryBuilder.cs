using System;
using System.Linq.Expressions;
using Lab4.Entities;
using Lab4.Models;

namespace Lab4.Builders
{
    public static class QueryBuilder
    {
        public static Expression<Func<Client, bool>> Build(FilterData filter)
        {
            var param = Expression.Parameter(typeof(Client), "c");
            Expression? body = null;

            if (filter.MinAge.HasValue && filter.MinAge.Value != 0)
            {
                var prop = Expression.Property(param, nameof(Client.Age));
                var val = Expression.Constant(filter.MinAge.Value);
                var cond = Expression.GreaterThanOrEqual(prop, val);
                body = body == null ? cond : Expression.AndAlso(body, cond);
            }

            if (filter.MinDiscount.HasValue && filter.MinDiscount.Value != 0)
            {
                var prop = Expression.Property(param, nameof(Client.Discount));
                var val = Expression.Constant(filter.MinDiscount.Value);
                var cond = Expression.GreaterThanOrEqual(prop, val);
                body = body == null ? cond : Expression.AndAlso(body, cond);
            }

            if (filter.IsCorporate.HasValue)
            {
                var prop = Expression.Property(param, nameof(Client.IsCorporate));
                var val = Expression.Constant(filter.IsCorporate.Value);
                var cond = Expression.Equal(prop, val);
                body = body == null ? cond : Expression.AndAlso(body, cond);
            }

            if (body == null)
                return Expression.Lambda<Func<Client, bool>>(Expression.Constant(true), param);

            return Expression.Lambda<Func<Client, bool>>(body, param);
        }
    }
}