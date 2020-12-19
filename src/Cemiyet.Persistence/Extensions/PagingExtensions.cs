using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cemiyet.Core;
using Microsoft.EntityFrameworkCore;

namespace Cemiyet.Persistence.Extensions
{
    public abstract class PageableModel
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = Constants.PageSize;
    }

    public static class PagingExtensions
    {
        public static async Task<List<T>> PagedToListAsync<T>(this IQueryable<T> query,
                                                              int page = 1, int pageSize = Constants.PageSize)
        {
            var skip = (page - 1) * pageSize;

            return await query.Skip(skip).Take(pageSize).ToListAsync();
        }

        public static ICollection<T> PagedToList<T>(this IEnumerable<T> query,
                                                    int page = 1, int pageSize = Constants.PageSize)
        {
            var skip = (page - 1) * pageSize;

            return query.Skip(skip).Take(pageSize).ToList();
        }
    }
}
