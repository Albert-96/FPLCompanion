using System.Linq.Expressions;

namespace PrimeNGTableExtension.Models
{
    public class EntityExpressionModel<T>
    {
        public Expression<Func<T, bool>> MainExpression { get; set; }
        public Expression<Func<T, bool>> ChildExpression { get; set; }
        public ParameterExpression ParameterExpression { get; set; }
        public Type EntityType { get; set; }
        public Expression<Func<T, bool>> Expressions { get; set; }
        public IQueryable<T> queryExpression { get; set; }
    }
}
