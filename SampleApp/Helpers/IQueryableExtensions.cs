using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApp.Helpers
{
    public static class IQueryableExtensions
    {
        /// <summary>
        ///     The to paginated list.
        /// </summary>
        /// <param name="query">
        ///     The query.
        /// </param>
        /// <param name="pageIndex">
        ///     The page index.
        /// </param>
        /// <param name="pageSize">
        ///     The page size.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        ///     The <see cref="Task" />.
        /// </returns>
        public static async Task<PaginatedList<T>> ToPaginatedList<T>(this IQueryable<T> query, int pageIndex,
            int pageSize)
        {
            var totalCount = await query.CountAsync();
            var collection = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);

            return new PaginatedList<T>(pageIndex, pageSize, totalCount, collection);
        }
    }
}
