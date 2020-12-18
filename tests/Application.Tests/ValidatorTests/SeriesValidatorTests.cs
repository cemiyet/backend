using System;
using System.Collections.Generic;
using Cemiyet.Application.Series.Commands.Add;
using Cemiyet.Application.Series.Commands.AddBook;
using Cemiyet.Application.Series.Commands.DeleteBook;
using Cemiyet.Application.Series.Commands.DeleteMany;
using Cemiyet.Application.Series.Commands.DeleteOne;
using Cemiyet.Application.Series.Commands.Update;
using Cemiyet.Application.Series.Commands.UpdatePartially;
using Cemiyet.Application.Series.Queries.List;
using Cemiyet.Application.Series.Queries.Details;
using FluentValidation.TestHelper;
using Xunit;

namespace Cemiyet.Application.Tests.ValidatorTests
{
    public class SeriesValidatorTests
    {
        private readonly ListQueryValidator _listQueryValidator;
        private readonly DetailsQueryValidator _detailsQueryValidator;

        private readonly AddCommandValidator _addCommandValidator;
        private readonly UpdateCommandValidator _updateCommandValidator;
        private readonly UpdatePartiallyCommandValidator _updatePartiallyCommandValidator;

        private readonly DeleteOneCommandValidator _deleteOneCommandValidator;
        private readonly DeleteManyCommandValidator _deleteManyCommandValidator;

        private readonly AddBookCommandValidator _addBookCommandValidator;
        private readonly DeleteBookCommandValidator _deleteBookCommandValidator;

        public SeriesValidatorTests()
        {
            _listQueryValidator = new ListQueryValidator();
            _detailsQueryValidator = new DetailsQueryValidator();

            _addCommandValidator = new AddCommandValidator();
            _updateCommandValidator = new UpdateCommandValidator();
            _updatePartiallyCommandValidator = new UpdatePartiallyCommandValidator();

            _deleteOneCommandValidator = new DeleteOneCommandValidator();
            _deleteManyCommandValidator = new DeleteManyCommandValidator();

            _addBookCommandValidator = new AddBookCommandValidator();
            _deleteBookCommandValidator = new DeleteBookCommandValidator();
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
        [InlineData("", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer a elementum tortor. Nullam vel ligula vitae dui efficitur gravida eu sit amet erat. Ut scelerisque hendrerit arcu vitae interdum. Ut rhoncus aliquet ipsum, ac laoreet mauris facilisis in. Donec in mattis ligula. Donec massa ipsum, cursus vel tincidunt eget, volutpat sed enim. Nunc mauris nunc, mollis et massa sit amet, mattis suscipit velit.Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer a elementum tortor. Nullam vel ligula vitae dui efficitur gravida eu sit amet erat. Ut scelerisque hendrerit arcu vitae interdum. Ut rhoncus aliquet ipsum, ac laoreet mauris facilisis in. Donec in mattis ligula. Donec massa ipsum, cursus vel tincidunt eget, volutpat sed enim. Nunc mauris nunc, mollis et massa sit amet, mattis suscipit velit.Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer a elementum tortor. Nullam vel ligula vitae dui efficitur gravida eu sit amet erat. Ut scelerisque hendrerit arcu vitae interdum. Ut rhoncus aliquet ipsum, ac laoreet mauris facilisis in. Donec in mattis ligula. Donec massa ipsum, cursus vel tincidunt eget, volutpat sed enim. Nunc mauris nunc, mollis et massa sit amet, mattis suscipit velit.Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer a elementum tortor. Nullam vel ligula vitae dui efficitur gravida eu sit amet erat. Ut scelerisque hendrerit arcu vitae interdum. Ut rhoncus aliquet ipsum, ac laoreet mauris facilisis in. Donec in mattis ligula. Donec massa ipsum, cursus vel tincidunt eget, volutpat sed enim. Nunc mauris nunc, mollis et massa sit amet, mattis suscipit velit.Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer a elementum tortor. Nullam vel ligula vitae dui efficitur gravida eu sit amet erat. Ut scelerisque hendrerit arcu vitae interdum. Ut rhoncus aliquet ipsum, ac laoreet mauris facilisis in. Donec in mattis ligula. Donec massa ipsum, cursus vel tincidunt eget, volutpat sed enim. Nunc mauris nunc, mollis et massa sit amet, mattis suscipit velit.")]
        [InlineData("Donec ac augue vel diam iaculis commodo. Lorem ipsum dolor sit amet, consectetur adipiscing elit.Donec ac augue vel diam iaculis commodo.", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer a elementum tortor. Nullam vel ligula vitae dui efficitur gravida eu sit amet erat. Ut scelerisque hendrerit arcu vitae interdum. Ut rhoncus aliquet ipsum, ac laoreet mauris facilisis in. Donec in mattis ligula. Donec massa ipsum, cursus vel tincidunt eget, volutpat sed enim. Nunc mauris nunc, mollis et massa sit amet, mattis suscipit velit.Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer a elementum tortor. Nullam vel ligula vitae dui efficitur gravida eu sit amet erat. Ut scelerisque hendrerit arcu vitae interdum. Ut rhoncus aliquet ipsum, ac laoreet mauris facilisis in. Donec in mattis ligula. Donec massa ipsum, cursus vel tincidunt eget, volutpat sed enim. Nunc mauris nunc, mollis et massa sit amet, mattis suscipit velit.Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer a elementum tortor. Nullam vel ligula vitae dui efficitur gravida eu sit amet erat. Ut scelerisque hendrerit arcu vitae interdum. Ut rhoncus aliquet ipsum, ac laoreet mauris facilisis in. Donec in mattis ligula. Donec massa ipsum, cursus vel tincidunt eget, volutpat sed enim. Nunc mauris nunc, mollis et massa sit amet, mattis suscipit velit.Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer a elementum tortor. Nullam vel ligula vitae dui efficitur gravida eu sit amet erat. Ut scelerisque hendrerit arcu vitae interdum. Ut rhoncus aliquet ipsum, ac laoreet mauris facilisis in. Donec in mattis ligula. Donec massa ipsum, cursus vel tincidunt eget, volutpat sed enim. Nunc mauris nunc, mollis et massa sit amet, mattis suscipit velit.Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer a elementum tortor. Nullam vel ligula vitae dui efficitur gravida eu sit amet erat. Ut scelerisque hendrerit arcu vitae interdum. Ut rhoncus aliquet ipsum, ac laoreet mauris facilisis in. Donec in mattis ligula. Donec massa ipsum, cursus vel tincidunt eget, volutpat sed enim. Nunc mauris nunc, mollis et massa sit amet, mattis suscipit velit.")]
        public void AddCommand_ShouldHave_ValidationErrors(string titleValue, string descriptionValue)
        {
            _addCommandValidator.ShouldHaveValidationErrorFor(x => x.Title, titleValue);
            _addCommandValidator.ShouldHaveValidationErrorFor(x => x.Description, descriptionValue);
        }

        [Theory]
        [InlineData("Lorem ipsum dolor sit.", "")]
        [InlineData("Lorem ipsum dolor sit.", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec ac augue vel diam iaculis commodo. Curabitur finibus enim eget sagittis vestibulum. Suspendisse vulputate ultrices posuere.")]
        public void AddCommand_ShouldNotHave_ValidationErrors(string titleValue, string descriptionValue)
        {
            _addCommandValidator.ShouldNotHaveValidationErrorFor(x => x.Title, titleValue);
            _addCommandValidator.ShouldNotHaveValidationErrorFor(x => x.Description, descriptionValue);
        }

        [Fact]
        public void AddBookCommand_ShouldHave_ValidationErrors()
        {
            _addBookCommandValidator.ShouldHaveValidationErrorFor(x => x.Books, new Dictionary<Guid, short>());
            _addBookCommandValidator.ShouldHaveValidationErrorFor(x => x.Books, new Dictionary<Guid, short>
            {
                {Guid.NewGuid(), 0}
            });
            _addBookCommandValidator.ShouldHaveValidationErrorFor(x => x.Books, new Dictionary<Guid, short>
            {
                {Guid.Empty, 5}
            });
        }

        [Fact]
        public void AddBookCommand_ShouldNotHave_ValidationErrors()
        {
            _addBookCommandValidator.ShouldNotHaveValidationErrorFor(x => x.Id, Guid.NewGuid());
            _addBookCommandValidator.ShouldNotHaveValidationErrorFor(x => x.Books, new Dictionary<Guid, short>
            {
                {Guid.NewGuid(), 2}
            });
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
                Description = "Desc"
            };
            ucValidator = _updateCommandValidator.TestValidate(ucWithNullData2);
            ucValidator.ShouldHaveValidationErrorFor(x => x.Title);
        }

        [Fact]
        public void UpdateCommand_ShouldNotHave_ValidationErrors()
        {
            _updateCommandValidator.ShouldNotHaveValidationErrorFor(x => x.Id, Guid.NewGuid());

            var ucWithGoodData = new UpdateCommand
            {
                Title = "Title",
                Description = "Desc"
            };

            var ucValidator = _updateCommandValidator.TestValidate(ucWithGoodData);
            ucValidator.ShouldNotHaveValidationErrorFor(x => x.Title);
            ucValidator.ShouldNotHaveValidationErrorFor(x => x.Description);
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
                Title = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec ac augue vel diam iaculis commodo. Curabitur finibus enim eget sagittis vestibulum. Suspendisse vulputate ultrices posuere.",
                Description =
                    @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec ac augue vel diam iaculis commodo.
                        Curabitur finibus enim eget sagittis vestibulum. Suspendisse vulputate ultrices posuere.
                        Praesent at elit lacus. Etiam eget lectus elementum, interdum leo et, congue arcu. Vivamus bibendum convallis
                        libero sit amet fringilla. Proin at nulla lorem. Donec euismod quis ex non faucibus.
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
            upcValidator.ShouldHaveValidationErrorFor(x => x.Title);
            upcValidator.ShouldHaveValidationErrorFor(x => x.Description);
        }

        [Fact]
        public void UpdatePartiallyCommand_ShouldNotHave_ValidationErrors()
        {
            var upcWithEmptyData = new UpdatePartiallyCommand { Title = "Başlık", Description = string.Empty};
            var upcValidator = _updatePartiallyCommandValidator.TestValidate(upcWithEmptyData);
            upcValidator.ShouldNotHaveValidationErrorFor(x => x.Title);
            upcValidator.ShouldNotHaveValidationErrorFor(x => x.Description);
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
        public void DeleteBookCommand_ShouldHave_ValidationErrors()
        {
            Guid[] ids = Array.Empty<Guid>();
            Guid[] ids2 = { Guid.Empty, Guid.Empty, Guid.Empty };

            _deleteBookCommandValidator.ShouldHaveValidationErrorFor(x => x.BookIds, ids);
            _deleteBookCommandValidator.ShouldHaveValidationErrorFor(x => x.BookIds, ids2);
        }

        [Fact]
        public void DeleteBookCommand_ShouldNotHave_ValidationErrors()
        {
            _deleteBookCommandValidator.ShouldNotHaveValidationErrorFor(x => x.Id, Guid.NewGuid());
            _deleteBookCommandValidator.ShouldNotHaveValidationErrorFor(x => x.BookIds, new[] { Guid.NewGuid() });
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

