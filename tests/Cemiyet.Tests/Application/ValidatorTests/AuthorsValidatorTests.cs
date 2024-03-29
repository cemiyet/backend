using System;
using Cemiyet.Application.Commands.Authors;
using Cemiyet.Application.Queries.Authors;
using FluentValidation.TestHelper;
using Xunit;

namespace Cemiyet.Tests.Application.ValidatorTests
{
    public class AuthorsValidatorTests
    {
        private readonly ListQueryValidator _listQueryValidator;
        private readonly ListBooksQueryValidator _listBooksQueryValidator;
        private readonly ListSeriesQueryValidator _listSeriesQueryValidator;
        private readonly DetailsQueryValidator _detailsQueryValidator;

        private readonly AddCommandValidator _addCommandValidator;
        private readonly UpdatePartiallyCommandValidator _updatePartiallyCommandValidator;
        private readonly UpdateCommandValidator _updateCommandValidator;
        private readonly DeleteOneCommandValidator _deleteOneCommandValidator;
        private readonly DeleteManyCommandValidator _deleteManyCommandValidator;

        public AuthorsValidatorTests()
        {
            _listQueryValidator = new ListQueryValidator();
            _listBooksQueryValidator = new ListBooksQueryValidator();
            _listSeriesQueryValidator = new ListSeriesQueryValidator();
            _detailsQueryValidator = new DetailsQueryValidator();

            _addCommandValidator = new AddCommandValidator();
            _updatePartiallyCommandValidator = new UpdatePartiallyCommandValidator();
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
        [InlineData(1, 1)]
        [InlineData(5, 50)]
        public void ListBooksQuery_ShouldNotHave_ValidationErrors(int pageValue, int pageSizeValue)
        {
            _listBooksQueryValidator.ShouldNotHaveValidationErrorFor(x => x.Id, Guid.NewGuid());
            _listBooksQueryValidator.ShouldNotHaveValidationErrorFor(x => x.Page, pageValue);
            _listBooksQueryValidator.ShouldNotHaveValidationErrorFor(x => x.PageSize, pageSizeValue);
        }

        [Theory]
        [InlineData(0, -1)]
        [InlineData(-1, 0)]
        public void ListSeriesQuery_ShouldHave_ValidationErrors(int pageValue, int pageSizeValue)
        {
            _listSeriesQueryValidator.ShouldHaveValidationErrorFor(x => x.Id, default(Guid));
            _listSeriesQueryValidator.ShouldHaveValidationErrorFor(x => x.Page, pageValue);
            _listSeriesQueryValidator.ShouldHaveValidationErrorFor(x => x.PageSize, pageSizeValue);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(5, 50)]
        public void ListSeriesQuery_ShouldNotHave_ValidationErrors(int pageValue, int pageSizeValue)
        {
            _listSeriesQueryValidator.ShouldNotHaveValidationErrorFor(x => x.Id, Guid.NewGuid());
            _listSeriesQueryValidator.ShouldNotHaveValidationErrorFor(x => x.Page, pageValue);
            _listSeriesQueryValidator.ShouldNotHaveValidationErrorFor(x => x.PageSize, pageSizeValue);
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
        public void AddCommand_ShouldHave_ValidationErrors()
        {
            _addCommandValidator.ShouldHaveValidationErrorFor(x => x.Name, "");
            _addCommandValidator.ShouldHaveValidationErrorFor(x => x.Name, "Y4Z4R");
            _addCommandValidator.ShouldHaveValidationErrorFor(x => x.Name, "Lorem ipsum dolor sit amet.");

            _addCommandValidator.ShouldHaveValidationErrorFor(x => x.Surname, "");
            _addCommandValidator.ShouldHaveValidationErrorFor(x => x.Surname, "V3L1");
            _addCommandValidator.ShouldHaveValidationErrorFor(x => x.Surname, "Lorem ipsum dolor sit amet.");

            _addCommandValidator.ShouldHaveValidationErrorFor(
                x => x.Bio,
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
                        neque, non efficitur mauris sem ac elit.");
        }

        [Fact]
        public void AddCommand_ShouldNotHave_ValidationErrors()
        {
            _addCommandValidator.ShouldNotHaveValidationErrorFor(x => x.Name, "Lorem ipsum dolor sit.");
            _addCommandValidator.ShouldNotHaveValidationErrorFor(x => x.Surname, "Lorem ipsum dolor sit.");

            _addCommandValidator.ShouldNotHaveValidationErrorFor(x => x.Bio, "");
            _addCommandValidator.ShouldNotHaveValidationErrorFor(
                x => x.Bio,
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
                        quam, vel tincidunt sem urna quis risus.");
        }

        [Fact]
        public void UpdatePartiallyCommand_ShouldHave_ValidationErrors()
        {
            var upcWithNullData = new UpdatePartiallyCommand();
            var upcValidator = _updatePartiallyCommandValidator.TestValidate(upcWithNullData);
            upcValidator.ShouldHaveValidationErrorFor(x => x.Name);
            upcValidator.ShouldHaveValidationErrorFor(x => x.Surname);
            upcValidator.ShouldHaveValidationErrorFor(x => x.Bio);

            var upcWithBadData = new UpdatePartiallyCommand
            {
                Name = "Y4Z4R",
                Surname = "V3L1",
                Bio =
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
            upcValidator.ShouldHaveValidationErrorFor(x => x.Name);
            upcValidator.ShouldHaveValidationErrorFor(x => x.Surname);
            upcValidator.ShouldHaveValidationErrorFor(x => x.Bio);

            var upcWithBadData2 = new UpdatePartiallyCommand
            {
                Name = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                Surname = "Lorem ipsum dolor sit amet, consectetur adipiscing elit."
            };
            upcValidator = _updatePartiallyCommandValidator.TestValidate(upcWithBadData2);
            upcValidator.ShouldHaveValidationErrorFor(x => x.Name);
            upcValidator.ShouldHaveValidationErrorFor(x => x.Surname);
        }

        [Fact]
        public void UpdatePartiallyCommand_ShouldNotHave_ValidationErrors()
        {
            var upcWithEmptyData = new UpdatePartiallyCommand { Name = "YAZAR", Surname = "", Bio = default };
            var upcValidator = _updatePartiallyCommandValidator.TestValidate(upcWithEmptyData);
            upcValidator.ShouldNotHaveValidationErrorFor(x => x.Name);
            upcValidator.ShouldNotHaveValidationErrorFor(x => x.Surname);
            upcValidator.ShouldNotHaveValidationErrorFor(x => x.Bio);

            var upcWithEmptyData2 = new UpdatePartiallyCommand
            {
                Name = null,
                Surname = "VELİ",
                Bio = ""
            };
            upcValidator = _updatePartiallyCommandValidator.TestValidate(upcWithEmptyData2);
            upcValidator.ShouldNotHaveValidationErrorFor(x => x.Name);
            upcValidator.ShouldNotHaveValidationErrorFor(x => x.Surname);
            upcValidator.ShouldNotHaveValidationErrorFor(x => x.Bio);

            var upcWithEmptyData3 = new UpdatePartiallyCommand
            {
                Name = default,
                Surname = null,
                Bio = "ABCDEF"
            };
            upcValidator = _updatePartiallyCommandValidator.TestValidate(upcWithEmptyData3);
            upcValidator.ShouldNotHaveValidationErrorFor(x => x.Name);
            upcValidator.ShouldNotHaveValidationErrorFor(x => x.Surname);
            upcValidator.ShouldNotHaveValidationErrorFor(x => x.Bio);
        }

        [Fact]
        public void UpdateCommand_ShouldHave_ValidationErrors()
        {
            var ucWithNullData = new UpdateCommand();
            var ucValidator = _updateCommandValidator.TestValidate(ucWithNullData);
            ucValidator.ShouldHaveValidationErrorFor(x => x.Name);
            ucValidator.ShouldHaveValidationErrorFor(x => x.Surname);
            ucValidator.ShouldHaveValidationErrorFor(x => x.Bio);

            var ucWithNullData2 = new UpdateCommand
            {
                Name = null,
                Surname = "Surname",
                Bio = default
            };
            ucValidator = _updateCommandValidator.TestValidate(ucWithNullData2);
            ucValidator.ShouldHaveValidationErrorFor(x => x.Name);
            ucValidator.ShouldHaveValidationErrorFor(x => x.Bio);

            var ucWithBadData = new UpdateCommand
            {
                Name = "Y4Z4R",
                Surname = "ucValidator.ShouldHaveValidationErrorFor(x => x.Surname);",
                Bio = "ABC"
            };
            ucValidator = _updateCommandValidator.TestValidate(ucWithBadData);
            ucValidator.ShouldHaveValidationErrorFor(x => x.Name);
            ucValidator.ShouldHaveValidationErrorFor(x => x.Surname);
        }

        [Fact]
        public void UpdateCommand_ShouldNotHave_ValidationErrors()
        {
            _updateCommandValidator.ShouldNotHaveValidationErrorFor(x => x.Id, Guid.NewGuid());

            var ucWithGoodData = new UpdateCommand
            {
                Name = "Name",
                Surname = "Surname",
                Bio = "Bio"
            };

            var ucValidator = _updateCommandValidator.TestValidate(ucWithGoodData);
            ucValidator.ShouldNotHaveValidationErrorFor(x => x.Name);
            ucValidator.ShouldNotHaveValidationErrorFor(x => x.Bio);
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
