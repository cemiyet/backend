using System.Collections.Generic;
using System.Linq;
using Cemiyet.Core.Entities;

namespace Cemiyet.Core.Extensions
{
    public static class SeriesExtensions
    {
        public static IEnumerable<Author> GetAuthors(this IEnumerable<SeriesBooks> query)
        {
            var uniqueAuthors = new HashSet<Author>();
            return query.SelectMany(sb => sb.Book.Authors.Select(ab => ab.Author)).Where(a => uniqueAuthors.Add(a));
        }

        public static IEnumerable<Serie> GetSeries(this IEnumerable<AuthorsBooks> query)
        {
            var uniqueSeries = new HashSet<Serie>();
            return query.SelectMany(ab => ab.Book.Series.Select(sb => sb.Serie)).Where(s => uniqueSeries.Add(s));
        }
    }
}
