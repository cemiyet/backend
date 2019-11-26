using System;
using Cemiyet.Application.Books.Queries.List;
using Cemiyet.Application.Books.Queries.ListEdition;
using Cemiyet.Application.Books.Queries.Details;
using Cemiyet.Application.Books.Queries.DetailsEdition;
using FluentValidation.TestHelper;
using Xunit;

namespace Cemiyet.Application.Tests.ValidatorTests
{
    public class BooksValidatorTests
    {
        private readonly ListQueryValidator _listQueryValidator;
        private readonly ListEditionQueryValidator _listEditionQueryValidator;
        private readonly DetailsQueryValidator _detailsQueryValidator;
        private readonly DetailsEditionQueryValidator _detailsEditionQueryValidator;

        public BooksValidatorTests()
        {
            _listQueryValidator = new ListQueryValidator();
            _listEditionQueryValidator = new ListEditionQueryValidator();
            _detailsQueryValidator = new DetailsQueryValidator();
            _detailsEditionQueryValidator = new DetailsEditionQueryValidator();
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

        [Fact]
        public void ListEditionQuery_ShouldHave_ValidationErrors()
        {
            _listEditionQueryValidator.ShouldHaveValidationErrorFor(x => x.Page, 0);
            _listEditionQueryValidator.ShouldHaveValidationErrorFor(x => x.Page, -1);

            _listEditionQueryValidator.ShouldHaveValidationErrorFor(x => x.PageSize, 0);
            _listEditionQueryValidator.ShouldHaveValidationErrorFor(x => x.PageSize, -1);
        }

        [Fact]
        public void ListEditionQuery_ShouldNotHave_ValidationErrors()
        {
            _listEditionQueryValidator.ShouldNotHaveValidationErrorFor(x => x.Id, Guid.NewGuid());
            _listEditionQueryValidator.ShouldNotHaveValidationErrorFor(x => x.Page, 5);
            _listEditionQueryValidator.ShouldNotHaveValidationErrorFor(x => x.PageSize, 50);
        }

        [Fact]
        public void DetailsQuery_ShouldHave_ValidationErrors()
        {
            _detailsQueryValidator.ShouldHaveValidationErrorFor(x => x.Id, default(Guid));
        }

        [Fact]
        public void DetailsQuery_ShouldNotHave_ValidationErrors()
        {
            _detailsQueryValidator.ShouldNotHaveValidationErrorFor(x => x.Id, Guid.NewGuid());
        }
    }
}

