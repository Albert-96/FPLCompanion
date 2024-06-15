using PrimeNGTableExtension.Core;
using PrimeNGTableExtension.Models;
using PrimeNGTableExtension.Utils;
using System.Data;

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

            if (!string.IsNullOrEmpty(request.SortField))
            {
                queryExpression = SingleOrderDataSet(queryExpression, request);
            }

            queryExpression = queryExpression.Skip(request.First.Value).Take(request.Rows.Value);

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

        private static IQueryable<T> SingleOrderDataSet<T>(
            IQueryable<T> queryExpression,
            TableRequestModel tableFilterPayload)
        {
            var expressionBuilder = new ExpressionBuilder<T>(queryExpression);
            switch (tableFilterPayload.SortOrder)
            {
                case (int)SortingEnum.OrderByAsc:
                    expressionBuilder.AddSortProperty(tableFilterPayload.SortField, false, false);
                    break;

                case (int)SortingEnum.OrderByDesc:
                    expressionBuilder.AddSortProperty(tableFilterPayload.SortField, true, false);
                    break;

                default:
                    throw new ArgumentException("Sort Order is invalid");
            }

            return expressionBuilder._Entity.queryExpression;
        }
    }
}
