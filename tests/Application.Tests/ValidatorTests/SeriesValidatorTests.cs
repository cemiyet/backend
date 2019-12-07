using System;
using System.Collections.Generic;
using Cemiyet.Application.Series.Commands.Add;
using Cemiyet.Application.Series.Commands.AddBook;
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
        private readonly AddBookCommandValidator _addBookCommandValidator;

        public SeriesValidatorTests()
        {
            _listQueryValidator = new ListQueryValidator();
            _detailsQueryValidator = new DetailsQueryValidator();

            _addCommandValidator = new AddCommandValidator();
            _addBookCommandValidator = new AddBookCommandValidator();
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
    }
}

