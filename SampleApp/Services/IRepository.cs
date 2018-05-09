using SampleApp.Helpers;
using SampleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SampleApp.Services
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        T Get(long id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties);

        Task<PaginatedList<T>> Paginate<TKey>(int pageIndex, int pageSize,
            Expression<Func<T, TKey>> keySelector);

        Task<PaginatedList<T>> Paginate<TKey>(
            int pageIndex,
            int pageSize,
            Expression<Func<T, TKey>> keySelector,
            Expression<Func<T, bool>> predicate,
            bool ascending = true,
            params Expression<Func<T, object>>[] includeProperties
        );
    }
}
