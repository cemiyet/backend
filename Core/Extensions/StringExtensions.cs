using System.Linq;
using System.Text.RegularExpressions;

namespace Cemiyet.Core.Extensions
{
    public static class StringExtensions
    {
        public static string ToSnakeCase(this string input)
        {
            if (string.IsNullOrEmpty(input)) return input;

            return Regex.Match(input, @"^_+") + Regex.Replace(input, @"([a-z0-9])([A-Z])", "$1_$2").ToLowerInvariant();
        }
    }
}
