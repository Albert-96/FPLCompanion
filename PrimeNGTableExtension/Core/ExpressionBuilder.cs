using PrimeNGTableExtension.Models;
using PrimeNGTableExtension.Utils;
using System.Linq.Expressions;

namespace PrimeNGTableExtension.Core
{
    public class ExpressionBuilder<T>
    {
        public readonly EntityExpressionModel<T> _Entity;

        public ExpressionBuilder(IQueryable<T> queryExpression)
        {
            _Entity = new EntityExpressionModel<T>()
            {
                EntityType = typeof(T),
                ParameterExpression = Expression.Parameter(typeof(T)),
                queryExpression = queryExpression
            };
        }

        public void AddFilterProperty(
            string propertyName,
            object propertyValue,
            string matchMode,
            string operation)
        {
            OperationsEnum operationsEnum;
            if (!Enum.TryParse(operation, true, out operationsEnum))
            {
                new ArgumentException($"{operation} is not part of defined enum for operations.");
            }

            var property = _Entity.EntityType.GetProperty(propertyName);
            var propertyType = property?.PropertyType;
            var propertyAccess = Expression.MakeMemberAccess(_Entity.ParameterExpression, property);
            var castValue = CastPropertyValue.CastPropertiesType(property, propertyValue.ToString());
            Expression expressionBody;
            ConstantExpression? filterValue;
            if (propertyType != typeof(DateTime))
            {
                filterValue = Expression.Constant(castValue, propertyType);
                expressionBody = ExpressionOperations.GetExpressionBody(matchMode, propertyAccess, filterValue);
            }
            else
            {
                var underlyingType = Nullable.GetUnderlyingType(propertyType);
                if (underlyingType != null)
                {
                    filterValue = Expression.Constant(((DateTime)castValue).Date.AddDays(1), underlyingType);
                    var nestedExpression = ForNestedProperty<T>($"{property.Name}.Value.Date", _Entity.ParameterExpression);
                    var expressionHasValue = Expression.NotEqual(propertyAccess, Expression.Constant(null, propertyType));
                    expressionBody = Expression.AndAlso(expressionHasValue, ExpressionOperations.GetDateExpressionBody(matchMode, nestedExpression, filterValue));
                }
                else
                {
                    filterValue = Expression.Constant(((DateTime)castValue).Date.AddDays(1), propertyType);
                    var nestedExpression = ForNestedProperty<T>($"{property.Name}.Date", _Entity.ParameterExpression);
                    expressionBody = ExpressionOperations.GetDateExpressionBody(matchMode, nestedExpression, filterValue);
                }
            }

            _Entity.ChildExpression = AddLambdaExpression(_Entity.ChildExpression, operationsEnum, expressionBody);
        }

        public void AddSortProperty(
            string propertyName,
            bool isDescending,
            bool isThenBy)
        {
            var property = _Entity.EntityType.GetProperty(propertyName);
            var propertyAccess = Expression.MakeMemberAccess(_Entity.ParameterExpression, property);
            var orderByExpression = Expression.Lambda(propertyAccess, _Entity.ParameterExpression);
            var command = ExpressionOperations.GetOrderByConstant(isDescending, isThenBy);
            var resultExpression = Expression.Call(
                typeof(Queryable), command,
                [_Entity.EntityType, property.PropertyType],
                _Entity.queryExpression.Expression,
                Expression.Quote(orderByExpression));
            _Entity.queryExpression = _Entity.queryExpression.Provider.CreateQuery<T>(resultExpression);
        } 

        public Expression<Func<T, bool>> CombineLambdaExpression(
            Expression<Func<T, bool>> queryExpression,
            OperationsEnum operation,
            Expression<Func<T, bool>> expression)
        {
            if (queryExpression == null)
            {
                return expression;
            }
            else
            {
                switch (operation)
                {
                    case OperationsEnum.AND:
                        expression = Expression.Lambda<Func<T, bool>>(
                            Expression.AndAlso(queryExpression.Body, expression.Body),
                            _Entity.ParameterExpression);
                        return expression;

                    case OperationsEnum.OR:
                        expression = Expression.Lambda<Func<T, bool>>(
                            Expression.OrElse(queryExpression.Body, expression.Body),
                            _Entity.ParameterExpression);
                        return expression;
                    default:
                        return queryExpression;
                }
            }
        }

        private Expression<Func<T, bool>> AddLambdaExpression(
            Expression<Func<T, bool>> queryExpression,
            OperationsEnum operation,
            Expression expressionBody)
        {
            var lambdaExpression = Expression.Lambda<Func<T, bool>>
                (expressionBody, _Entity.ParameterExpression);
            if (queryExpression == null)
            {
                return lambdaExpression;
            }
            else
            {
                switch (operation)
                {
                    case OperationsEnum.AND:
                        lambdaExpression = Expression.Lambda<Func<T, bool>>(
                            Expression.AndAlso(queryExpression.Body, lambdaExpression.Body),
                            _Entity.ParameterExpression);
                        return lambdaExpression;

                    case OperationsEnum.OR:
                        lambdaExpression = Expression.Lambda<Func<T, bool>>(
                            Expression.OrElse(queryExpression.Body, lambdaExpression.Body),
                            _Entity.ParameterExpression);
                        return lambdaExpression;
                    default:
                        return queryExpression;
                }
            }
        }

        private static Expression ForNestedProperty<T>(string columnName, ParameterExpression param)
        {
            Expression property = columnName.Split('.')
                .Aggregate<string, Expression>
                (param, (c, m) => Expression.Property(c, m));
            return property;
        }
    }
}
