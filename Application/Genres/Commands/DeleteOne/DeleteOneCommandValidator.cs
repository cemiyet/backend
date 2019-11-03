using FluentValidation;

namespace Cemiyet.Application.Genres.Commands.DeleteOne
{
    public class DeleteOneCommandValidator : AbstractValidator<DeleteOneCommand>
    {
        public DeleteOneCommandValidator()
        {
            RuleFor(doc => doc.Id).NotEmpty();
        }
    }
}
