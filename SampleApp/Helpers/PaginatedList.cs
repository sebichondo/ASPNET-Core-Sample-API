using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SampleApp.Helpers
{
    public class PaginatedList<T>
    {
        private readonly List<T> _items;

        /// <summary>
        ///     Gets the page index.
        /// </summary>
        [JsonProperty]
        public int PageIndex { get; private set; }

        /// <summary>
        ///     Gets the page size.
        /// </summary>
        [JsonProperty]
        public int PageSize { get; private set; }

        /// <summary>
        ///     Gets the total count.
        /// </summary>
        [JsonProperty]
        public int TotalCount { get; private set; }

        /// <summary>
        ///     Gets the total page count.
        /// </summary>
        [JsonProperty]
        public int TotalPageCount { get; private set; }

        [JsonProperty]
        public List<T> Items => _items;

        /// <summary>
        ///     Initializes a new instance of the <see cref="PaginatedList{T}" /> class.
        /// </summary>
        /// <param name="pageIndex">
        ///     The page index.
        /// </param>
        /// <param name="pageSize">
        ///     The page size.
        /// </param>
        /// <param name="totalCount">
        ///     The total count.
        /// </param>
        /// <param name="source">
        ///     The source.
        /// </param>
        public PaginatedList(int pageIndex, int pageSize, int totalCount, IQueryable<T> source)
        {
            _items = source.ToList();
            PageIndex = pageIndex == 0 ? 1 : pageIndex;
            PageSize = pageSize;
            TotalCount = totalCount;
            TotalPageCount = (int)Math.Ceiling(totalCount / (double)pageSize);
        }

        /// <summary>
        ///     Gets a value indicating whether has previous page.
        /// </summary>
        [JsonProperty]
        public bool HasPreviousPage
        {
            get { return PageIndex > 1; }
        }

        /// <summary>
        ///     Gets a value indicating whether has next page.
        /// </summary>
        [JsonProperty]
        public bool HasNextPage
        {
            get { return PageIndex < TotalPageCount; }
        }
    }
}
