using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using SampleApp.Data;
using SampleApp.Helpers;
using SampleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SampleApp.Services
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext context;
        private DbSet<T> entities;
        string errorMessage = string.Empty;
        public long totalRecords = 0;

        public Repository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return Enumerable.AsEnumerable(entities);
        }

        public T Get(long id)
        {
            return Queryable.SingleOrDefault(entities, s => s.Id == id);
        }
        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            context.Update(entity);
            context.SaveChanges();
        }

        public virtual IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            var query = entities.AsQueryable();
            return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        public virtual async Task<PaginatedList<T>> Paginate<TKey>(int pageIndex, int pageSize,
            Expression<Func<T, TKey>> keySelector)
        {
            return await Paginate(pageIndex, pageSize, keySelector, null);
        }

        public virtual async Task<PaginatedList<T>> Paginate<TKey>(
            int pageIndex,
            int pageSize,
            Expression<Func<T, TKey>> keySelector,
            Expression<Func<T, bool>> predicate,
            bool ascending = true,
            params Expression<Func<T, object>>[] includeProperties
        )
        {
            IQueryable<T> query = null;
            query = @ascending ? AllIncluding(includeProperties).OrderBy(keySelector) : AllIncluding(includeProperties).OrderByDescending(keySelector);
            query = (predicate == null) ? query : query.Where(predicate);
            return await query.ToPaginatedList(pageIndex, pageSize);
        }

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            entities.Remove(entity);
            context.SaveChanges();
        }
    }

    public static class Ext
    {
        public static IIncludableQueryable<T, object> IncludeChild<T>(this IQueryable<T> source, Expression<Func<T, object>> includeProperty) where T : BaseEntity
        {
            return source.Include(includeProperty);
        }
    }
}
