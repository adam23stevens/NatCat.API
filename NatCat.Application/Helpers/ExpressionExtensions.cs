using System.Linq.Expressions;

namespace NatCat.Application.Helpers
{
    internal static class ExpressionExtensions
    {
        internal static Expression<Func<T,bool>> And<T>(this Expression<Func<T,bool>> left, Expression<Func<T, bool>> right)
        {
            if (left == null) return right;
            var and = Expression.And(left.Body, right.Body);
            var ret = Expression.Lambda<Func<T, bool>>(and, left.Parameters.Single());

            return ret;
        }

        internal static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
        {
            if (left == null) return right;
            var and = Expression.OrElse(left.Body, right.Body);
            return Expression.Lambda<Func<T, bool>>(and, left.Parameters.Single());
        }
    }
}