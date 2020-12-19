using Cemiyet.Core.Extensions;
using Xunit;

namespace Cemiyet.Tests.Core
{
    public class StringExtensionsTests
    {
        [Fact]
        public void ToSnakeCase_ShouldNotConvert_EmptyString()
        {
            Assert.Equal(string.Empty, string.Empty.ToSnakeCase());
            Assert.Null(default(string).ToSnakeCase());
        }

        [Fact]
        public void ToSnakeCase_ShouldConvert_CamelCase()
        {
            Assert.Equal("lower_camel_case", "lowerCamelCase".ToSnakeCase());
            Assert.Equal("upper_camel_case", "UpperCamelCase".ToSnakeCase());
        }

        [Fact]
        public void ToSnakeCase_ShouldConvert_ALLCAPS()
        {
            Assert.Equal("allcaps", "ALLCAPS".ToSnakeCase());
            Assert.Equal("all_caps", "ALL_CAPS".ToSnakeCase());
        }

        [Fact]
        public void ToSnakeCase_ShouldConvert_UnknownCase()
        {
            Assert.Equal("i-dont_know_what_this_case_is", "I-DontKnowWhat_thisCase_is".ToSnakeCase());
        }
    }
}
