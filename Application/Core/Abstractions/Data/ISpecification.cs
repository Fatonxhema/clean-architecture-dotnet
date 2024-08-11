using Domain.Core.Primitives;
using System.Linq.Expressions;

namespace Application.Core.Abstractions.Data
{
    public interface ISpecification<T> where T : Entity
    {
        Expression<Func<T, bool>> ToExpression();
    }
}