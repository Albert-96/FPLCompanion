using PrimeNGTableExtension.Utils;
using System.Linq.Expressions;
using System.Reflection;

namespace PrimeNGTableExtension.Core
{
    public static class ExpressionOperations
    {
        public static Expression GetExpressionBody(
            string matchMode,
            MemberExpression propertyAccess,
            ConstantExpression filterValue)
        {
            MethodInfo? method;
            switch (matchMode)
            {
                case PrimeNGConstants.MatchModeEquals:
                    return Expression.Equal(propertyAccess, filterValue);

                case PrimeNGConstants.MatchModeNotEquals:
                    return Expression.NotEqual(propertyAccess, filterValue);

                case PrimeNGConstants.MatchModeGreaterThan:
                    return Expression.GreaterThan(propertyAccess, filterValue);

                case PrimeNGConstants.MatchModeLessThan:
                    return Expression.LessThan(propertyAccess, filterValue);

                case PrimeNGConstants.MatchModeGreaterOrEqualsThan:
                case PrimeNGConstants.MatchModeAfter:
                    return Expression.GreaterThanOrEqual(propertyAccess, filterValue);

                case PrimeNGConstants.MatchModeLessOrEqualsThan:
                case PrimeNGConstants.MatchModeBefore:
                    return Expression.LessThanOrEqual(propertyAccess, filterValue);

                case PrimeNGConstants.MatchModeContains:
                    method = typeof(string).GetMethod("Contains", [typeof(string)]);
                    return Expression.Call(propertyAccess, method, filterValue);

                case PrimeNGConstants.MatchModeNotContains:
                    method = typeof(string).GetMethod("Contains", [typeof(string)]);
                    return Expression.Not(Expression.Call(propertyAccess, method, filterValue));

                case PrimeNGConstants.MatchModeStartsWith:
                    method = typeof(string).GetMethod("StartsWith", [typeof(string)]);
                    return Expression.Call(propertyAccess, method, filterValue);

                case PrimeNGConstants.MatchModeEndsWith:
                    method = typeof(string).GetMethod("EndsWith", [typeof(string)]);
                    return Expression.Call(propertyAccess, method, filterValue);

                default:
                    throw new NotSupportedException($"Filter: {matchMode} is not supported.");
            }
        }

        public static Expression GetDateExpressionBody(
            string matchMode,
            Expression expression,
            ConstantExpression? filterValue)
        {
            switch (matchMode)
            {
                case PrimeNGConstants.MatchModeDateIs:
                case PrimeNGConstants.MatchModeIs:
                    return Expression.Equal(expression, filterValue);

                case PrimeNGConstants.MatchModeDateIsNot:
                case PrimeNGConstants.MatchModeIsNot:
                    return Expression.NotEqual(expression, filterValue);

                case PrimeNGConstants.MatchModeDateBefore:
                    return Expression.LessThan(expression, filterValue);

                case PrimeNGConstants.MatchModeDateAfter:
                    return Expression.GreaterThan(expression, filterValue);

                default:
                    throw new NotSupportedException($"Filter: {matchMode} is not supported.");
            }
        }
    }
}
