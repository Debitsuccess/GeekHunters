using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace GRS.ApplicationCore.Interfaces
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }
        List<Expression<Func<T, object>>> Includes { get; }
        void AddInclude(Expression<Func<T, object>> includeExpression);
    }
}
