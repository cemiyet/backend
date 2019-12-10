using System;
using System.Collections.Generic;
using Cemiyet.Application.Books.Commands.Add;
using Cemiyet.Application.Books.Commands.AddEdition;
using Cemiyet.Application.Books.Commands.UpdatePartially;
using Cemiyet.Application.Books.Commands.Update;
using Cemiyet.Application.Books.Commands.UpdateEdition;
using Cemiyet.Application.Books.Commands.DeleteOne;
using Cemiyet.Application.Books.Commands.DeleteOneEdition;
using Cemiyet.Application.Books.Commands.DeleteMany;
using Cemiyet.Application.Books.Commands.DeleteManyEdition;
using Cemiyet.Application.Books.Commands.UpdatePartiallyEdition;
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

        private readonly UpdateCommandValidator _updateCommandValidator;
        private readonly UpdateEditionCommandValidator _updateEditionCommandValidator;

        private readonly UpdatePartiallyCommandValidator _updatePartiallyCommandValidator;
        private readonly UpdatePartiallyEditionCommandValidator _updatePartiallyEditionCommandValidator;

        private readonly DeleteOneCommandValidator _deleteOneCommandValidator;
        private readonly DeleteOneEditionCommandValidator _deleteOneEditionCommandValidator;

        private readonly DeleteManyCommandValidator _deleteManyCommandValidator;
        private readonly DeleteManyEditionCommandValidator _deleteManyEditionCommandValidator;

        public BooksValidatorTests()
        {
            _listQueryValidator = new ListQueryValidator();
            _listEditionQueryValidator = new ListEditionQueryValidator();
            _detailsQueryValidator = new DetailsQueryValidator();
            _detailsEditionQueryValidator = new DetailsEditionQueryValidator();

            _addCommandValidator = new AddCommandValidator();
            _addEditionCommandValidator = new AddEditionCommandValidator();

            _updateCommandValidator = new UpdateCommandValidator();
            _updateEditionCommandValidator = new UpdateEditionCommandValidator();

            _updatePartiallyCommandValidator = new UpdatePartiallyCommandValidator();
            _updatePartiallyEditionCommandValidator = new UpdatePartiallyEditionCommandValidator();

            _deleteOneCommandValidator = new DeleteOneCommandValidator();
            _deleteOneEditionCommandValidator = new DeleteOneEditionCommandValidator();

            _deleteManyCommandValidator = new DeleteManyCommandValidator();
            _deleteManyEditionCommandValidator = new DeleteManyEditionCommandValidator();
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
        public void ListEditionQuery_ShouldHave_ValidationErrors(int pageValue, int pageSizeValue)
        {
            _listEditionQueryValidator.ShouldHaveValidationErrorFor(x => x.Page, pageValue);
            _listEditionQueryValidator.ShouldHaveValidationErrorFor(x => x.PageSize, pageSizeValue);
        }

        [Theory]
        [InlineData(5, 50)]
        [InlineData(50, 5)]
        public void ListEditionQuery_ShouldNotHave_ValidationErrors(int pageValue, int pageSizeValue)
        {
            _listEditionQueryValidator.ShouldNotHaveValidationErrorFor(x => x.Id, Guid.NewGuid());
            _listEditionQueryValidator.ShouldNotHaveValidationErrorFor(x => x.Page, pageValue);
            _listEditionQueryValidator.ShouldNotHaveValidationErrorFor(x => x.PageSize, pageSizeValue);
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
            _addEditionCommandValidator.ShouldHaveValidationErrorFor(x => x.BooksId, default(Guid));
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
            _addEditionCommandValidator.ShouldNotHaveValidationErrorFor(x => x.BooksId, Guid.NewGuid());
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

        [Fact]
        public void DeleteManyEditionCommand_ShouldHave_ValidationErrors()
        {
            string[] isbns = { };
            string[] isbns2 = { Guid.NewGuid().ToString() };
            string[] isbns3 = { Guid.NewGuid().ToString(), "Guid.Empty", "" };

            _deleteManyEditionCommandValidator.ShouldHaveValidationErrorFor(x => x.Isbns, isbns);
            _deleteManyEditionCommandValidator.ShouldHaveValidationErrorFor(x => x.Isbns, isbns2);
            _deleteManyEditionCommandValidator.ShouldHaveValidationErrorFor(x => x.Isbns, isbns3);
        }

        [Fact]
        public void DeleteManyEditionCommand_ShouldNotHave_ValidationErrors()
        {
            string[] isbns = { "1234567890111", "0123456789111" };

            _deleteManyEditionCommandValidator.ShouldNotHaveValidationErrorFor(x => x.Isbns, isbns);
        }

        [Fact]
        public void UpdateCommand_ShouldHave_ValidationErrors()
        {
            var ucWithNullData = new UpdateCommand();
            var ucValidator = _updateCommandValidator.TestValidate(ucWithNullData);
            ucValidator.ShouldHaveValidationErrorFor(x => x.Title);
            ucValidator.ShouldHaveValidationErrorFor(x => x.Description);

            var ucWithNullData2 = new UpdateCommand
            {
                Title = null,
                Description = ""
            };
            ucValidator = _updateCommandValidator.TestValidate(ucWithNullData2);
            ucValidator.ShouldHaveValidationErrorFor(x => x.Title);
            ucValidator.ShouldHaveValidationErrorFor(x => x.Description);

            var ucWithBadData = new UpdateCommand
            {
                Title = "ucValidator.ShouldHaveValidationErrorFor(x => x.Title);ucValidator.ShouldHaveValidationErrorFor(x => x.Description);",
                Description = default(string)
            };
            ucValidator = _updateCommandValidator.TestValidate(ucWithBadData);
            ucValidator.ShouldHaveValidationErrorFor(x => x.Title);
            ucValidator.ShouldHaveValidationErrorFor(x => x.Description);
        }

        [Fact]
        public void UpdateCommand_ShouldNotHave_ValidationErrors()
        {
            _updateCommandValidator.ShouldNotHaveValidationErrorFor(x => x.Id, Guid.NewGuid());

            var ucWithGoodData = new UpdateCommand
            {
                Title = "Title",
                Description = "Description"
            };

            var ucValidator = _updateCommandValidator.TestValidate(ucWithGoodData);
            ucValidator.ShouldNotHaveValidationErrorFor(x => x.Title);
            ucValidator.ShouldNotHaveValidationErrorFor(x => x.Description);
        }

        [Fact]
        public void UpdateEditionCommand_ShouldHave_ValidationErrors()
        {
            var uecWithNullData = new UpdateEditionCommand();
            var uecValidator = _updateEditionCommandValidator.TestValidate(uecWithNullData);
            uecValidator.ShouldHaveValidationErrorFor(x => x.PageCount);
            uecValidator.ShouldHaveValidationErrorFor(x => x.PrintDate);
            uecValidator.ShouldHaveValidationErrorFor(x => x.PublishersId);
            uecValidator.ShouldHaveValidationErrorFor(x => x.BooksId);
            uecValidator.ShouldHaveValidationErrorFor(x => x.DimensionsId);

            var uecWithNullData2 = new UpdateEditionCommand
            {
                Id = Guid.Empty,
                Isbn = null,
                PageCount = 0
            };

            uecValidator = _updateEditionCommandValidator.TestValidate(uecWithNullData2);
            uecValidator.ShouldHaveValidationErrorFor(x => x.PageCount);
            uecValidator.ShouldHaveValidationErrorFor(x => x.PrintDate);
            uecValidator.ShouldHaveValidationErrorFor(x => x.PublishersId);
            uecValidator.ShouldHaveValidationErrorFor(x => x.BooksId);
            uecValidator.ShouldHaveValidationErrorFor(x => x.DimensionsId);
        }

        [Fact]
        public void UpdateEditionCommand_ShouldNotHave_ValidationErrors()
        {
            _updateEditionCommandValidator.ShouldNotHaveValidationErrorFor(x => x.Id, Guid.NewGuid());

            var uecWithGoodData = new UpdateEditionCommand
            {
                Id = Guid.NewGuid(),
                Isbn = "1234567890111",
                PageCount = 255,
                PrintDate = DateTime.Now,
                PublishersId = Guid.NewGuid(),
                BooksId = Guid.NewGuid(),
                DimensionsId = Guid.NewGuid()
            };

            var uecValidator = _updateEditionCommandValidator.TestValidate(uecWithGoodData);
            uecValidator.ShouldNotHaveValidationErrorFor(x => x.Id);
            uecValidator.ShouldNotHaveValidationErrorFor(x => x.Isbn);
            uecValidator.ShouldNotHaveValidationErrorFor(x => x.PageCount);
            uecValidator.ShouldNotHaveValidationErrorFor(x => x.PrintDate);
            uecValidator.ShouldNotHaveValidationErrorFor(x => x.PublishersId);
            uecValidator.ShouldNotHaveValidationErrorFor(x => x.BooksId);
            uecValidator.ShouldNotHaveValidationErrorFor(x => x.DimensionsId);
        }

        [Fact]
        public void UpdatePartiallyCommand_ShouldHave_ValidationErrors()
        {
            var upcWithNullData = new UpdatePartiallyCommand();
            var upcValidator = _updatePartiallyCommandValidator.TestValidate(upcWithNullData);
            upcValidator.ShouldHaveValidationErrorFor(x => x.Title);
            upcValidator.ShouldHaveValidationErrorFor(x => x.Description);

            var upcWithBadData = new UpdatePartiallyCommand
            {
                Description =
                    @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec ac augue vel diam iaculis commodo.
                        Curabitur finibus enim eget sagittis vestibulum. Suspendisse vulputate ultrices posuere.
                        Praesent at elit lacus. Etiam eget lectus elementum, interdum leo et, congue arcu. Vivamus bibendum convallis
                        libero sit amet fringilla. Proin at nulla lorem. Proin non justo tortor.Praesent in urna eu neque
                        elementum blandit.Nam pellentesque purus at eleifend vulputate.Maecenas pharetra rutrum auctor.Maecenas
                        ut auctor tortor, id egestas velit.In placerat augue vel libero placerat, vel posuere ex tincidunt.Fusce
                        pellentesque iaculis ex, vestibulum sollicitudin enim lobortis pretium.
                        Maecenas iaculis lectus sit amet vehicula pretium. Donec euismod quis ex non faucibus.
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
                        neque, non efficitur mauris sem ac elit."
            };
            upcValidator = _updatePartiallyCommandValidator.TestValidate(upcWithBadData);
            upcValidator.ShouldHaveValidationErrorFor(x => x.Description);

            var upcWithBadData2 = new UpdatePartiallyCommand
            {
                Title = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec ac augue vel diam iaculis commodo. Curabitur finibus enim eget sagittis vestibulum.",
                Description = "123"
            };
            upcValidator = _updatePartiallyCommandValidator.TestValidate(upcWithBadData2);
            upcValidator.ShouldHaveValidationErrorFor(x => x.Title);
        }

        [Fact]
        public void UpdatePartiallyCommand_ShouldNotHave_ValidationErrors()
        {
            var upcWithEmptyData = new UpdatePartiallyCommand { Title = "YAZAR", Description = default };
            var upcValidator = _updatePartiallyCommandValidator.TestValidate(upcWithEmptyData);
            upcValidator.ShouldNotHaveValidationErrorFor(x => x.Title);
            upcValidator.ShouldNotHaveValidationErrorFor(x => x.Description);

            var upcWithEmptyData2 = new UpdatePartiallyCommand
            {
                Title = null,
                Description = "AÇIKLAMA"
            };
            upcValidator = _updatePartiallyCommandValidator.TestValidate(upcWithEmptyData2);
            upcValidator.ShouldNotHaveValidationErrorFor(x => x.Title);
            upcValidator.ShouldNotHaveValidationErrorFor(x => x.Description);
        }

        [Fact]
        public void UpdatePartiallyEditionCommand_ShouldHave_ValidationErrors()
        {
            var upecWithBadData = new UpdatePartiallyEditionCommand
            {
                Isbn = "0123456789",
                NewIsbn = "1234567890"
            };

            var upecValidator = _updatePartiallyEditionCommandValidator.TestValidate(upecWithBadData);
            upecValidator.ShouldHaveValidationErrorFor(x => x.Isbn);
            upecValidator.ShouldHaveValidationErrorFor(x => x.NewIsbn);
            upecValidator.ShouldHaveValidationErrorFor(x => x.PageCount);
            upecValidator.ShouldHaveValidationErrorFor(x => x.PrintDate);
            upecValidator.ShouldHaveValidationErrorFor(x => x.BooksId);
            upecValidator.ShouldHaveValidationErrorFor(x => x.DimensionsId);
            upecValidator.ShouldHaveValidationErrorFor(x => x.PublishersId);
        }

        [Fact]
        public void UpdatePartiallyEditionCommand_ShouldNotHave_ValidationErrors()
        {
            var upecWithEmptyData = new UpdatePartiallyEditionCommand
            {
                NewIsbn = "1234567890111",
                PageCount = short.MaxValue,
                PrintDate = DateTime.UtcNow,
            };

            var upecValidator = _updatePartiallyEditionCommandValidator.TestValidate(upecWithEmptyData);
            upecValidator.ShouldNotHaveValidationErrorFor(x => x.Id);
            upecValidator.ShouldNotHaveValidationErrorFor(x => x.Isbn);
            upecValidator.ShouldNotHaveValidationErrorFor(x => x.NewIsbn);
            upecValidator.ShouldNotHaveValidationErrorFor(x => x.PageCount);
            upecValidator.ShouldNotHaveValidationErrorFor(x => x.PrintDate);
            upecValidator.ShouldNotHaveValidationErrorFor(x => x.BooksId);
            upecValidator.ShouldNotHaveValidationErrorFor(x => x.DimensionsId);
            upecValidator.ShouldNotHaveValidationErrorFor(x => x.PublishersId);
        }
    }
}

