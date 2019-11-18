using System;
using Cemiyet.Application.Dimensions.Commands.Add;
using Cemiyet.Application.Dimensions.Commands.DeleteMany;
using Cemiyet.Application.Dimensions.Commands.DeleteOne;
using Cemiyet.Application.Dimensions.Commands.Update;
using Cemiyet.Application.Dimensions.Commands.UpdatePartially;
using Cemiyet.Application.Dimensions.Queries.Details;
using Cemiyet.Application.Dimensions.Queries.List;
using FluentValidation.TestHelper;
using Xunit;

namespace Cemiyet.Application.Tests.ValidatorTests
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
        public void DetailsQuery_ShouldHave_ValidationErrors()
        {
            _detailsQueryValidator.ShouldHaveValidationErrorFor(x => x.Id, default(Guid));
        }

        [Fact]
        public void DetailsQuery_ShouldNotHave_ValidationErrors()
        {
            _detailsQueryValidator.ShouldNotHaveValidationErrorFor(x => x.Id, Guid.NewGuid());
        }

        [Fact]
        public void UpdateCommand_ShouldHave_ValidationErrors()
        {
            _updateCommandValidator.ShouldHaveValidationErrorFor(x => x.Width, 0);
            _updateCommandValidator.ShouldHaveValidationErrorFor(x => x.Height, 0);
        }

        [Fact]
        public void UpdateCommand_ShouldNotHave_ValidationErrors()
        {
            _updateCommandValidator.ShouldNotHaveValidationErrorFor(x => x.Id, Guid.NewGuid());
            _updateCommandValidator.ShouldNotHaveValidationErrorFor(x => x.Width, 12.5);
            _updateCommandValidator.ShouldNotHaveValidationErrorFor(x => x.Height, 22.3);
        }

        [Fact]
        public void UpdatePartiallyCommand_ShouldHave_ValidationErrors()
        {
            _updatePartiallyCommandValidator.ShouldHaveValidationErrorFor(x => x.Width, 0);
            _updatePartiallyCommandValidator.ShouldHaveValidationErrorFor(x => x.Height, 0);
        }

        [Fact]
        public void UpdatePartiallyCommand_ShouldNotHave_ValidationErrors()
        {
            _updatePartiallyCommandValidator.ShouldNotHaveValidationErrorFor(x => x.Width, 1);
            _updatePartiallyCommandValidator.ShouldNotHaveValidationErrorFor(x => x.Height, 1);

            var upc1 = new UpdatePartiallyCommand { Width = 1 };
            var res1 = _updatePartiallyCommandValidator.TestValidate(upc1);
            res1.ShouldNotHaveValidationErrorFor(x => x.Width);
            res1.ShouldNotHaveValidationErrorFor(x => x.Height);

            var upc2 = new UpdatePartiallyCommand { Height = 1 };
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
            Guid[] ids = { };
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

        [Fact]
        public void AddCommand_ShouldHave_ValidationErrors()
        {
            _addCommandValidator.ShouldHaveValidationErrorFor(x => x.Width, 0);
            _addCommandValidator.ShouldHaveValidationErrorFor(x => x.Height, 0);
        }

        [Fact]
        public void AddCommand_ShouldNotHave_ValidationErrors()
        {
            _addCommandValidator.ShouldNotHaveValidationErrorFor(x => x.Width, 12.5);
            _addCommandValidator.ShouldNotHaveValidationErrorFor(x => x.Height, 22.3);
        }
    }
}
