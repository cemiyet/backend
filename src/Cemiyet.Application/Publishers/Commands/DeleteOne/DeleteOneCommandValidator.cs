using FluentValidation;

namespace Cemiyet.Application.Publishers.Commands.DeleteOne
{
    public class DeleteOneCommandValidator : AbstractValidator<DeleteOneCommand>
    {
        public DeleteOneCommandValidator()
        {
            RuleFor(doc => doc.Id).NotEmpty();
        }
    }
}
