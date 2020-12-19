using System;
using Cemiyet.Application.Commands.Dimensions;
using Cemiyet.Application.Queries.Dimensions;
using FluentValidation.TestHelper;
using Xunit;

namespace Cemiyet.Tests.Application.ValidatorTests
{
    public class DimensionsValidatorTests
    {
        private readonly ListQueryValidator _listQueryValidator;
        private readonly DetailsQueryValidator _detailsQueryValidator;

        private readonly AddCommandValidator _addCommandValidator;
        private readonly UpdateCommandValidator _updateCommandValidator;
        private readonly UpdatePartiallyCommandValidator _updatePartiallyCommandValidator;
        private readonly DeleteOneCommandValidator _deleteOneCommandValidator;
        private readonly DeleteManyCommandValidator _deleteManyCommandValidator;

        public DimensionsValidatorTests()
        {
            _listQueryValidator = new ListQueryValidator();
            _detailsQueryValidator = new DetailsQueryValidator();

            _addCommandValidator = new AddCommandValidator();
            _updateCommandValidator = new UpdateCommandValidator();
            _updatePartiallyCommandValidator = new UpdatePartiallyCommandValidator();
            _deleteOneCommandValidator = new DeleteOneCommandValidator();
            _deleteManyCommandValidator = new DeleteManyCommandValidator();
        }

        [Theory]
        [InlineData(0, -1)]
        [InlineData(-1, 0)]
        public void ListQuery_ShouldHave_ValidationErrors(int pageValue, int pageSizeValue)
        {
            _listQueryValidator.ShouldHaveValidationErrorFor(x => x.Page, pageValue);
            _listQueryValidator.ShouldHaveValidationErrorFor(x => x.PageSize, pageSizeValue);
        }

        [Theory]
        [InlineData(5, 50)]
        [InlineData(50, 5)]
        public void ListQuery_ShouldNotHave_ValidationErrors(int pageValue, int pageSizeValue)
        {
            _listQueryValidator.ShouldNotHaveValidationErrorFor(x => x.Page, pageValue);
            _listQueryValidator.ShouldNotHaveValidationErrorFor(x => x.PageSize, pageSizeValue);
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

        [Theory]
        [InlineData(1, 1)]
        [InlineData(0, 0)]
        [InlineData(-1, -1)]
        public void AddCommand_ShouldHave_ValidationErrors(double widthValue, double heightValue)
        {
            _addCommandValidator.ShouldHaveValidationErrorFor(x => x.Width, widthValue);
            _addCommandValidator.ShouldHaveValidationErrorFor(x => x.Height, heightValue);
        }

        [Theory]
        [InlineData(12.5, 2)]
        [InlineData(1.1, 22.3)]
        [InlineData(15, 20)]
        public void AddCommand_ShouldNotHave_ValidationErrors(double widthValue, double heightValue)
        {
            _addCommandValidator.ShouldNotHaveValidationErrorFor(x => x.Width, widthValue);
            _addCommandValidator.ShouldNotHaveValidationErrorFor(x => x.Height, heightValue);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(0, 0)]
        [InlineData(-1, -1)]
        public void UpdateCommand_ShouldHave_ValidationErrors(double widthValue, double heightValue)
        {
            _updateCommandValidator.ShouldHaveValidationErrorFor(x => x.Width, widthValue);
            _updateCommandValidator.ShouldHaveValidationErrorFor(x => x.Height, heightValue);
        }

        [Theory]
        [InlineData(2, 2)]
        [InlineData(1.1, 1.1)]
        public void UpdateCommand_ShouldNotHave_ValidationErrors(double widthValue, double heightValue)
        {
            _updateCommandValidator.ShouldNotHaveValidationErrorFor(x => x.Id, Guid.NewGuid());
            _updateCommandValidator.ShouldNotHaveValidationErrorFor(x => x.Width, widthValue);
            _updateCommandValidator.ShouldNotHaveValidationErrorFor(x => x.Height, heightValue);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(0, 0)]
        [InlineData(-1, -1)]
        public void UpdatePartiallyCommand_ShouldHave_ValidationErrors(double widthValue, double heightValue)
        {
            _updatePartiallyCommandValidator.ShouldHaveValidationErrorFor(x => x.Width, widthValue);
            _updatePartiallyCommandValidator.ShouldHaveValidationErrorFor(x => x.Height, heightValue);
        }

        [Theory]
        [InlineData(2, 2)]
        [InlineData(1.1, 1.1)]
        public void UpdatePartiallyCommand_ShouldNotHave_ValidationErrors(double widthValue, double heightValue)
        {
            _updatePartiallyCommandValidator.ShouldNotHaveValidationErrorFor(x => x.Width, widthValue);
            _updatePartiallyCommandValidator.ShouldNotHaveValidationErrorFor(x => x.Height, heightValue);

            var upc1 = new UpdatePartiallyCommand { Width = widthValue };
            var res1 = _updatePartiallyCommandValidator.TestValidate(upc1);
            res1.ShouldNotHaveValidationErrorFor(x => x.Width);
            res1.ShouldNotHaveValidationErrorFor(x => x.Height);

            var upc2 = new UpdatePartiallyCommand { Height = heightValue };
            var res2 = _updatePartiallyCommandValidator.TestValidate(upc2);
            res2.ShouldNotHaveValidationErrorFor(x => x.Width);
            res2.ShouldNotHaveValidationErrorFor(x => x.Height);
        }

        [Fact]
        public void DeleteOneCommand_ShouldHave_ValidationErrors()
        {
            _deleteOneCommandValidator.ShouldHaveValidationErrorFor(x => x.Id, default(Guid));
        }

        [Fact]
        public void DeleteOneCommand_ShouldNotHave_ValidationErrors()
        {
            _deleteOneCommandValidator.ShouldNotHaveValidationErrorFor(x => x.Id, Guid.NewGuid());
        }

        [Fact]
        public void DeleteManyCommand_ShouldHave_ValidationErrors()
        {
            Guid[] ids = Array.Empty<Guid>();
            Guid[] ids2 = { Guid.NewGuid() };
            Guid[] ids3 = { Guid.Empty, Guid.Empty, Guid.Empty };

            _deleteManyCommandValidator.ShouldHaveValidationErrorFor(x => x.Ids, ids);
            _deleteManyCommandValidator.ShouldHaveValidationErrorFor(x => x.Ids, ids2);
            _deleteManyCommandValidator.ShouldHaveValidationErrorFor(x => x.Ids, ids3);
        }

        [Fact]
        public void DeleteManyCommand_ShouldNotHave_ValidationErrors()
        {
            Guid[] ids = { Guid.NewGuid(), Guid.NewGuid() };

            _deleteManyCommandValidator.ShouldNotHaveValidationErrorFor(x => x.Ids, ids);
        }
    }
}
