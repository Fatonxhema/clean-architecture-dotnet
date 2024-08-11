using Application.Core.Abstractions.Data;
using Domain.Core.Primitives;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    /// <summary>
    /// Represents the generic repository with the most common repository methods.
    /// </summary>
    /// <typeparam name="T">The entity type.</typeparam>
    public class GenericRepository<T> : IGenericRepository<T> where T : Entity
    {
        private readonly EventReminderDbContext _context;

        public GenericRepository(EventReminderDbContext context)
        {
            _context = context;
        }

        public async Task<Maybe<T>> GetByIdAsync(Guid id)
        {
            try
            {
                var entity = await _context.Set<T>().FindAsync(id);
                return entity != null ? Maybe<T>.From(entity) : Maybe<T>.None;
            }
            catch (Exception)
            {
                return Maybe<T>.None;
            }
        }

        public async Task<Maybe<IList<T>>> GetAllAsync(int pageNumber, int pageSize)
        {
            try
            {
                var entities = await _context.Set<T>()
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return entities.Any() ? Maybe<IList<T>>.From(entities) : Maybe<IList<T>>.None;
            }
            catch (Exception)
            {
                return Maybe<IList<T>>.None;
            }
        }

        public async Task<Maybe<IList<T>>> FindAsync(ISpecification<T> specification, IOrderedQueryable<T> order, int pageNumber, int pageSize)
        {
            try
            {
                var query = _context.Set<T>().Where(specification.ToExpression());

                //if (order != null)
                //{
                //    query = order(query);
                //}

                var entities = await query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return entities.Any() ? Maybe<IList<T>>.From(entities) : Maybe<IList<T>>.None;
            }
            catch (Exception)
            {
                return Maybe<IList<T>>.None;
            }
        }
        public async Task<Maybe<PaginatedList<T>>> GetPaginated(int pageNumber, int pageSize)
        {
            try
            {
                var count = await _context.Set<T>().CountAsync();
                var items = await _context.Set<T>()
                                                    .AsQueryable()
                                                    .Skip((pageNumber - 1) * pageSize)
                                                    .Take(pageSize)
                                                    .ToListAsync();
                return new PaginatedList<T>(items, count, pageNumber, pageSize);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<Maybe<PaginatedList<T>>> GetPaginatedAndSorted(int pageNumber, int pageSize, string search)
        {
            try
            {
                var count = await _context.Set<T>().CountAsync();

                var items = await _context.Set<T>()
                                                    .AsQueryable()
                                                    .Skip((pageNumber - 1) * pageSize)
                                                    .Take(pageSize)
                                                    .ToListAsync();
                return new PaginatedList<T>(items, count, pageNumber, pageSize);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<Maybe<PaginatedList<T>>> GetPagedAsync(
            int pageIndex, int pageSize,
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null
        )
        {
            var count = await _context.Set<T>().CountAsync();
            IQueryable<T> query = _context.Set<T>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            var items = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            var paginatedList = new PaginatedList<T>(items, count, pageIndex, pageSize);
            return paginatedList.Items.Any() ? Maybe<PaginatedList<T>>.From(paginatedList) : Maybe<PaginatedList<T>>.None;

        }
        public async Task<Maybe<IList<T>>> GetPaginatedAsync(Expression<Func<T, bool>> filter, IOrderedQueryable<T> order, int pageNumber, int pageSize)
        {
            try
            {
                IQueryable<T> query = _context.Set<T>();

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                //if (order != null)
                //{
                //    query = order(query);
                //}

                var entities = await query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return entities.Any() ? Maybe<IList<T>>.From(entities) : Maybe<IList<T>>.None;
            }
            catch (Exception)
            {
                return Maybe<IList<T>>.None;
            }
        }

        public async Task<Maybe<T>> CreateAsync(T entity)
        {
            try
            {
                var addedEntity = _context.Set<T>().Add(entity);
                await _context.SaveChangesAsync();
                return Maybe<T>.From(addedEntity.Entity);
            }
            catch (Exception)
            {
                return Maybe<T>.None;
            }
        }

        public async Task<Maybe<T>> UpdateAsync(T entity)
        {
            try
            {
                _context.Set<T>().Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Maybe<T>.From(entity);
            }
            catch (Exception)
            {
                return Maybe<T>.None;
            }
        }

        public async Task<Maybe<T>> DeleteAsync(Guid id)
        {
            try
            {
                var entity = await _context.Set<T>().FindAsync(id);
                if (entity == null)
                {
                    return Maybe<T>.None;
                }

                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
                return Maybe<T>.From(entity);
            }
            catch (Exception)
            {
                return Maybe<T>.None;
            }
        }

        public async Task<Maybe<IList<T>>> GetWithExpressionAsync(Func<T, bool> predicate)
        {
            try
            {
                var entities = _context.Set<T>().Where(predicate).ToList();
                return entities.Any() ? Maybe<IList<T>>.From(entities) : Maybe<IList<T>>.None;
            }
            catch (Exception)
            {
                return Maybe<IList<T>>.None;
            }
        }
    }
}

