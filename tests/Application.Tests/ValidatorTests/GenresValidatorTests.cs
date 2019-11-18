using System;
using Cemiyet.Application.Genres.Commands.Add;
using Cemiyet.Application.Genres.Commands.DeleteMany;
using Cemiyet.Application.Genres.Commands.DeleteOne;
using Cemiyet.Application.Genres.Commands.Update;
using Cemiyet.Application.Genres.Queries.Details;
using Cemiyet.Application.Genres.Queries.List;
using FluentValidation.TestHelper;
using Xunit;

namespace Cemiyet.Application.Tests.ValidatorTests
{
    public class GenresValidatorTests
    {
        private readonly ListQueryValidator _listQueryValidator;
        private readonly DetailsQueryValidator _detailsQueryValidator;

        private readonly AddCommandValidator _addCommandValidator;
        private readonly UpdateCommandValidator _updateCommandValidator;
        private readonly DeleteOneCommandValidator _deleteOneCommandValidator;
        private readonly DeleteManyCommandValidator _deleteManyCommandValidator;

        public GenresValidatorTests()
        {
            _listQueryValidator = new ListQueryValidator();
            _detailsQueryValidator = new DetailsQueryValidator();

            _addCommandValidator = new AddCommandValidator();
            _updateCommandValidator = new UpdateCommandValidator();
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
            _updateCommandValidator.ShouldHaveValidationErrorFor(x => x.Name, "");
            _updateCommandValidator.ShouldHaveValidationErrorFor(x => x.Name, "AN1");
            _updateCommandValidator.ShouldHaveValidationErrorFor(x => x.Name,
                                                                 "Lorem ipsum dolor sit amet, consectetur massa nunc.");
        }

        [Fact]
        public void UpdateCommand_ShouldNotHave_ValidationErrors()
        {
            _updateCommandValidator.ShouldNotHaveValidationErrorFor(x => x.Id, Guid.NewGuid());
            _updateCommandValidator.ShouldNotHaveValidationErrorFor(x => x.Name, "Anı");
            _updateCommandValidator.ShouldNotHaveValidationErrorFor(x => x.Name, "Bilim Kurgu");
            _updateCommandValidator.ShouldNotHaveValidationErrorFor(x => x.Name, "Antoloji / Derleme");
            _updateCommandValidator.ShouldNotHaveValidationErrorFor(x => x.Name,
                                                                    "Lorem ipsum dolor sit amet, consectetur massa nunc");
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
            _addCommandValidator.ShouldHaveValidationErrorFor(x => x.Name, "");
            _addCommandValidator.ShouldHaveValidationErrorFor(x => x.Name, "AN1");
            _addCommandValidator.ShouldHaveValidationErrorFor(x => x.Name,
                                                              "Lorem ipsum dolor sit amet, consectetur massa nunc.");
        }

        [Fact]
        public void AddCommand_ShouldNotHave_ValidationErrors()
        {
            _addCommandValidator.ShouldNotHaveValidationErrorFor(x => x.Name, "Anı");
            _addCommandValidator.ShouldNotHaveValidationErrorFor(x => x.Name, "Bilim Kurgu");
            _addCommandValidator.ShouldNotHaveValidationErrorFor(x => x.Name, "Antoloji / Derleme");
            _addCommandValidator.ShouldNotHaveValidationErrorFor(x => x.Name,
                                                                 "Lorem ipsum dolor sit amet, consectetur massa nunc");
        }
    }
}
