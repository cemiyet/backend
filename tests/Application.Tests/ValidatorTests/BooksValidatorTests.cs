using System;
using System.Collections.Generic;
using Cemiyet.Application.Books.Commands.Add;
using Cemiyet.Application.Books.Commands.AddEdition;
using Cemiyet.Application.Books.Commands.DeleteOne;
using Cemiyet.Application.Books.Commands.DeleteOneEdition;
using Cemiyet.Application.Books.Commands.DeleteMany;
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

        private readonly AddCommandValidator _addCommandValidator;
        private readonly AddEditionCommandValidator _addEditionCommandValidator;

        private readonly DeleteOneCommandValidator _deleteOneCommandValidator;
        private readonly DeleteOneEditionCommandValidator _deleteOneEditionCommandValidator;

        private readonly DeleteManyCommandValidator _deleteManyCommandValidator;

        public BooksValidatorTests()
        {
            _listQueryValidator = new ListQueryValidator();
            _listEditionQueryValidator = new ListEditionQueryValidator();
            _detailsQueryValidator = new DetailsQueryValidator();
            _detailsEditionQueryValidator = new DetailsEditionQueryValidator();

            _addCommandValidator = new AddCommandValidator();
            _addEditionCommandValidator = new AddEditionCommandValidator();

            _deleteOneCommandValidator = new DeleteOneCommandValidator();
            _deleteOneEditionCommandValidator = new DeleteOneEditionCommandValidator();

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

        [Fact]
        public void DetailsEditionQuery_ShouldHave_ValidationErrors()
        {
            _detailsEditionQueryValidator.ShouldHaveValidationErrorFor(x => x.Id, default(Guid));
            _detailsEditionQueryValidator.ShouldHaveValidationErrorFor(x => x.Isbn, default(Guid).ToString());
        }

        [Fact]
        public void DetailsEditionQuery_ShouldNotHave_ValidationErrors()
        {
            _detailsEditionQueryValidator.ShouldNotHaveValidationErrorFor(x => x.Id, Guid.NewGuid());
            _detailsEditionQueryValidator.ShouldNotHaveValidationErrorFor(x => x.Isbn, "0123456789111");
        }

        [Fact]
        public void AddCommand_ShouldHave_ValidationErrors()
        {
            _addCommandValidator.ShouldHaveValidationErrorFor(x => x.Title, "");
            _addCommandValidator.ShouldHaveValidationErrorFor(x => x.Description, "");
            _addCommandValidator.ShouldHaveValidationErrorFor(x => x.GenreIds, new List<Guid>());
            _addCommandValidator.ShouldHaveValidationErrorFor(x => x.AuthorIds, new List<Guid>());

            _addCommandValidator.ShouldHaveValidationErrorFor(
                x => x.Description,
                @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec ac augue vel diam iaculis commodo.
                        Curabitur finibus enim eget sagittis vestibulum. Suspendisse vulputate ultrices posuere.
                        Praesent at elit lacus. Etiam eget lectus elementum, interdum leo et, congue arcu. Vivamus bibendum convallis
                        libero sit amet fringilla. Proin at nulla lorem. Integer nec diam dictum, cursus nunc
                        quis, blandit leo.Quisque arcu tortor, aliquam quis urna id, efficitur hendrerit nunc.Maecenas
                        quis justo et ex congue ultricies.Curabitur posuere, nibh consequat lobortis
                        faucibus, massa mauris faucibus lacus, vitae pellentesque sem sapien et purus.
                        Quisque nec tincidunt nunc, non pharetra magna.Donec euismod quis ex non faucibus.
                        Nulla velit ligula, egestas vel enim eu, auctor dignissim quam.
                        Curabitur vel mattis nisi.Aliquam a pharetra nisl.Proin non justo tortor.Praesent in urna eu neque
                        elementum blandit.Nam pellentesque purus at eleifend vulputate.Maecenas pharetra rutrum auctor.Maecenas
                        ut auctor tortor, id egestas velit.In placerat augue vel libero placerat, vel posuere ex tincidunt.Fusce
                        pellentesque iaculis ex, vestibulum sollicitudin enim lobortis pretium.
                        Maecenas iaculis lectus sit amet vehicula pretium.In hac habitasse platea dictumst.Nullam
                        molestie dictum dolor, dapibus commodo ligula.Integer nec diam dictum, cursus nunc
                        quis, blandit leo.Quisque arcu tortor, aliquam quis urna id, efficitur hendrerit nunc.Maecenas
                        quis justo et ex congue ultricies.Curabitur posuere, nibh consequat lobortis
                        faucibus, massa mauris faucibus lacus, vitae pellentesque sem sapien et purus.
                        Quisque nec tincidunt nunc, non pharetra magna.Donec vulputate ligula in augue feugiat congue.Mauris
                        gravida feugiat ornare.Maecenas rutrum, lectus in ultrices accumsan, dui nulla pretium
                        quam, vel tincidunt sem urna quis risus.Nunc libero neque, porta et blandit
                        vel, finibus a purus.Suspendisse ornare, tortor sodales tempus luctus, enim neque eleifend
                        neque, non efficitur mauris sem ac elit.");
        }

        [Fact]
        public void AddCommand_ShouldNotHave_ValidationErrors()
        {
            _addCommandValidator.ShouldNotHaveValidationErrorFor(x => x.Title, "Lorem ipsum dolor sit.");
            _addCommandValidator.ShouldNotHaveValidationErrorFor(x => x.Description, "Lorem ipsum dolor sit.");
            _addCommandValidator.ShouldNotHaveValidationErrorFor(x => x.GenreIds, new List<Guid> { new Guid(), new Guid() });
            _addCommandValidator.ShouldNotHaveValidationErrorFor(x => x.AuthorIds, new List<Guid> { new Guid(), new Guid() });
        }

        [Fact]
        public void AddEditionCommand_ShouldHave_ValidationErrors()
        {
            _addEditionCommandValidator.ShouldHaveValidationErrorFor(x => x.Isbn, "");
            _addEditionCommandValidator.ShouldHaveValidationErrorFor(x => x.PageCount, default(short));
            _addEditionCommandValidator.ShouldHaveValidationErrorFor(x => x.PrintDate, default(DateTime));
            _addEditionCommandValidator.ShouldHaveValidationErrorFor(x => x.Id, default(Guid));
            _addEditionCommandValidator.ShouldHaveValidationErrorFor(x => x.DimensionsId, default(Guid));
            _addEditionCommandValidator.ShouldHaveValidationErrorFor(x => x.PublishersId, default(Guid));
        }

        [Fact]
        public void AddEditionCommand_ShouldNotHave_ValidationErrors()
        {
            _addEditionCommandValidator.ShouldNotHaveValidationErrorFor(x => x.Isbn, "0123456789111");
            _addEditionCommandValidator.ShouldNotHaveValidationErrorFor(x => x.PageCount, short.MaxValue);
            _addEditionCommandValidator.ShouldNotHaveValidationErrorFor(x => x.PrintDate, DateTime.Now);
            _addEditionCommandValidator.ShouldNotHaveValidationErrorFor(x => x.Id, Guid.NewGuid());
            _addEditionCommandValidator.ShouldNotHaveValidationErrorFor(x => x.DimensionsId, Guid.NewGuid());
            _addEditionCommandValidator.ShouldNotHaveValidationErrorFor(x => x.PublishersId, Guid.NewGuid());
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
        public void DeleteOneEditionCommand_ShouldHave_ValidationErrors()
        {
            _deleteOneEditionCommandValidator.ShouldHaveValidationErrorFor(x => x.Isbn, "");
            _deleteOneEditionCommandValidator.ShouldHaveValidationErrorFor(x => x.Isbn, Guid.NewGuid().ToString());
            _deleteOneEditionCommandValidator.ShouldHaveValidationErrorFor(x => x.Isbn, "123456789011");
        }

        [Fact]
        public void DeleteOneEditionCommand_ShouldNotHave_ValidationErrors()
        {
            _deleteOneEditionCommandValidator.ShouldNotHaveValidationErrorFor(x => x.Isbn, "1234567890111");
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
    }
}

