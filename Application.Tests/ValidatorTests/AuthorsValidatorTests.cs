using Cemiyet.Application.Authors.Queries.List;
using FluentValidation.TestHelper;
using Xunit;

namespace Cemiyet.Application.Tests.ValidatorTests
{
    public class AuthorsValidatorTests
    {
        private readonly ListQueryValidator _listQueryValidator;

        public AuthorsValidatorTests()
        {
            _listQueryValidator = new ListQueryValidator();
        }

        [Fact]
        public void ListQuery_ShouldHave_ValidationErrors()
        {
            _listQueryValidator.ShouldHaveValidationErrorFor(x => x.Page, 0);
            _listQueryValidator.ShouldHaveValidationErrorFor(x => x.Page, -1);

            _listQueryValidator.ShouldHaveValidationErrorFor(x => x.PageSize, 0);
            _listQueryValidator.ShouldHaveValidationErrorFor(x => x.PageSize, -1);
        }

        [Fact]
        public void ListQuery_ShouldNotHave_ValidationErrors()
        {
            _listQueryValidator.ShouldNotHaveValidationErrorFor(x => x.Page, 5);
            _listQueryValidator.ShouldNotHaveValidationErrorFor(x => x.PageSize, 50);
        }
    }
}
