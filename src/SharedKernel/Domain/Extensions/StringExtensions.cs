using System.Text.RegularExpressions;

namespace Cemiyet.SharedKernel.Domain.Extensions;

public static class StringExtensions
{
    private static readonly Regex _regex = new(@"([a-z0-9])([A-Z])", RegexOptions.Compiled);

    public static string ToSnakeCase(this string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        return _regex.Replace(input, "$1_$2").ToLowerInvariant();
    }
}