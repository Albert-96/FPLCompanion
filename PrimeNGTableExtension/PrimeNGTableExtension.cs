using PrimeNGTableExtension.Core;
using PrimeNGTableExtension.Models;
using PrimeNGTableExtension.Utils;
using System;
using System.Reflection;

namespace PrimeNGTableExtension
{
    public static class PrimeNGTableExtension
    {
        public static IQueryable<T> PrimeNGTableQuery<T>
        (
            this IQueryable<T> queryExpression,
            TableRequestModel request)
        {
            var filters = request.Filters
                .Where(x => x.Value.Any(y => y.Value != null));

            if (filters.Any())
            {
                var expressionBuilder = new ExpressionBuilder<T>(queryExpression);
                foreach (var column in filters)
                {
                    var length = column.Value.Count;
                    for (int index = 0; index < length; index++)
                    {
                        if (index == 0 && expressionBuilder._Entity.ChildExpression != null)
                        {
                            expressionBuilder._Entity.MainExpression = expressionBuilder.CombineLambdaExpression(
                                    expressionBuilder._Entity.MainExpression,
                                    OperationsEnum.AND,
                                    expressionBuilder._Entity.ChildExpression);
                            expressionBuilder._Entity.ChildExpression = null;
                        }

                        expressionBuilder.AddFilterProperty(column.Key, column.Value[index].Value, column.Value[index].MatchMode, column.Value[index].Operator);

                        if (index == (column.Value.Count - 1) && expressionBuilder._Entity.ChildExpression != null)
                        {
                            expressionBuilder._Entity.MainExpression = expressionBuilder.CombineLambdaExpression(
                                    expressionBuilder._Entity.MainExpression,
                                    OperationsEnum.AND,
                                    expressionBuilder._Entity.ChildExpression);
                            expressionBuilder._Entity.ChildExpression = null;
                        }
                    }
                }

                queryExpression = queryExpression.Where(expressionBuilder._Entity.MainExpression);
            }

            return queryExpression;
        }

        public static int PrimeNGTableCount<T>
        (
            this IQueryable<T> queryExpression,
            TableRequestModel request)
        {
            // Set Where Expression for query
            var filters = request.Filters
                .Where(x => x.Value.Any(y => y.Value != null));

            if (filters.Any())
            {
                var expressionBuilder = new ExpressionBuilder<T>(queryExpression);
                //filters.SelectMany(
                //    x => x.Value,
                //    (x, filter) =>
                //    {
                //        expressionBuilder.AddFilterProperty(x.Key, filter.Value, filter.MatchMode, filter.Operator);
                //        return filter;
                //    }).ToList();
                filters.SelectMany(
                    x => x.Value.Select((filter, index) =>
                    {
                        if (index == 0 && expressionBuilder._Entity.ChildExpression != null)
                        {
                            expressionBuilder._Entity.MainExpression = expressionBuilder.CombineLambdaExpression(
                                    expressionBuilder._Entity.MainExpression,
                                    OperationsEnum.AND,
                                    expressionBuilder._Entity.ChildExpression);
                            expressionBuilder._Entity.ChildExpression = null;
                        }

                        expressionBuilder.AddFilterProperty(x.Key, filter.Value, filter.MatchMode, filter.Operator);

                        if (index == (x.Value.Count - 1) && expressionBuilder._Entity.ChildExpression != null)
                        {
                            expressionBuilder._Entity.MainExpression = expressionBuilder.CombineLambdaExpression(
                                    expressionBuilder._Entity.MainExpression,
                                    OperationsEnum.AND,
                                    expressionBuilder._Entity.ChildExpression);
                            expressionBuilder._Entity.ChildExpression = null;
                        }
                        return filter;
                    })).ToList();
                queryExpression = queryExpression.Where(expressionBuilder._Entity.MainExpression);
            }

            return queryExpression.Count();
        }
    }
}
