using System.Linq.Expressions;

namespace GestaoContas.Api.Extensions.Expressions
{
    public static class PredicateBuilder
    {
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            var parameter = Expression.Parameter(typeof(T));

            var combined = new ReplaceParameterVisitor
            {
                Source = second.Parameters[0],
                Target = parameter
            }.Visit(second.Body);

            return Expression.Lambda<Func<T, bool>>(
                Expression.AndAlso(first.Body, combined), parameter);
        }

        private class ReplaceParameterVisitor : ExpressionVisitor
        {
            public required ParameterExpression Source;
            public required ParameterExpression Target;

            protected override Expression VisitParameter(ParameterExpression node)
            {
                return node == Source ? Target : node;
            }
        }
    }
}
