using Domain.Core.Primitives;
using System.Linq.Expressions;

namespace Application.Core.Abstractions.Data
{
    public interface IGenericRepository<T> where T : Entity
    {
        Task<Maybe<T>> CreateAsync(T entity);
        Task<Maybe<T>> DeleteAsync(Guid id);
        Task<Maybe<IList<T>>> FindAsync(ISpecification<T> specification, IOrderedQueryable<T> order, int pageNumber, int pageSize);
        Task<Maybe<IList<T>>> GetAllAsync(int pageNumber, int pageSize);
        Task<Maybe<T>> GetByIdAsync(Guid id);
        Task<Maybe<IList<T>>> GetPaginatedAsync(Expression<Func<T, bool>> filter, IOrderedQueryable<T> order, int pageNumber, int pageSize);
        Task<Maybe<IList<T>>> GetWithExpressionAsync(Func<T, bool> predicate);
        Task<Maybe<T>> UpdateAsync(T entity);
    }
}