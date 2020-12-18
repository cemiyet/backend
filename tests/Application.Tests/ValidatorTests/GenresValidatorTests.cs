using System;
using Cemiyet.Application.Genres.Commands.Add;
using Cemiyet.Application.Genres.Commands.DeleteMany;
using Cemiyet.Application.Genres.Commands.DeleteOne;
using Cemiyet.Application.Genres.Commands.Update;
using Cemiyet.Application.Genres.Queries.List;
using Cemiyet.Application.Genres.Queries.ListBooks;
using Cemiyet.Application.Genres.Queries.Details;
using FluentValidation.TestHelper;
using Xunit;

namespace Cemiyet.Application.Tests.ValidatorTests
{
    public class GenresValidatorTests
    {
        private readonly ListQueryValidator _listQueryValidator;
        private readonly ListBooksQueryValidator _listBooksQueryValidator;
        private readonly DetailsQueryValidator _detailsQueryValidator;

        private readonly AddCommandValidator _addCommandValidator;
        private readonly UpdateCommandValidator _updateCommandValidator;
        private readonly DeleteOneCommandValidator _deleteOneCommandValidator;
        private readonly DeleteManyCommandValidator _deleteManyCommandValidator;

        public GenresValidatorTests()
        {
            _listQueryValidator = new ListQueryValidator();
            _listBooksQueryValidator = new ListBooksQueryValidator();
            _detailsQueryValidator = new DetailsQueryValidator();

            _addCommandValidator = new AddCommandValidator();
            _updateCommandValidator = new UpdateCommandValidator();
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

        [Theory]
        [InlineData(0, -1)]
        [InlineData(-1, 0)]
        public void ListBooksQuery_ShouldHave_ValidationErrors(int pageValue, int pageSizeValue)
        {
            _listBooksQueryValidator.ShouldHaveValidationErrorFor(x => x.Id, default(Guid));
            _listBooksQueryValidator.ShouldHaveValidationErrorFor(x => x.Page, pageValue);
            _listBooksQueryValidator.ShouldHaveValidationErrorFor(x => x.PageSize, pageSizeValue);
        }

        [Theory]
        [InlineData(5, 50)]
        [InlineData(50, 5)]
        public void ListBooksQuery_ShouldNotHave_ValidationErrors(int pageValue, int pageSizeValue)
        {
            _listBooksQueryValidator.ShouldNotHaveValidationErrorFor(x => x.Id, Guid.NewGuid());
            _listBooksQueryValidator.ShouldNotHaveValidationErrorFor(x => x.Page, pageValue);
            _listBooksQueryValidator.ShouldNotHaveValidationErrorFor(x => x.PageSize, pageSizeValue);
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
        [InlineData("")]
        [InlineData("AN1")]
        [InlineData("Lorem ipsum dolor sit amet, consectetur massa nunc.")]
        public void UpdateCommand_ShouldHave_ValidationErrors(string nameValue)
        {
            _updateCommandValidator.ShouldHaveValidationErrorFor(x => x.Name, nameValue);
        }

        [Theory]
        [InlineData("Anı")]
        [InlineData("Bilim Kurgu")]
        [InlineData("Antoloji / Derleme")]
        [InlineData("Lorem ipsum dolor sit amet, consectetur massa nunc")]
        public void UpdateCommand_ShouldNotHave_ValidationErrors(string nameValue)
        {
            _updateCommandValidator.ShouldNotHaveValidationErrorFor(x => x.Id, Guid.NewGuid());
            _updateCommandValidator.ShouldNotHaveValidationErrorFor(x => x.Name, nameValue);
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

        [Theory]
        [InlineData("")]
        [InlineData("AN1")]
        [InlineData("Lorem ipsum dolor sit amet, consectetur massa nunc.")]
        public void AddCommand_ShouldHave_ValidationErrors(string nameValue)
        {
            _addCommandValidator.ShouldHaveValidationErrorFor(x => x.Name, nameValue);
        }

        [Theory]
        [InlineData("Anı")]
        [InlineData("Bilim Kurgu")]
        [InlineData("Antoloji / Derleme")]
        [InlineData("Lorem ipsum dolor sit amet, consectetur massa nunc")]
        public void AddCommand_ShouldNotHave_ValidationErrors(string nameValue)
        {
            _addCommandValidator.ShouldNotHaveValidationErrorFor(x => x.Name, nameValue);
        }
    }
}
