using FluentValidation;
using MediatR;

namespace Cemiyet.Application.Commands.Books
{
    public class DeleteOneEditionCommand : IRequest
    {
        public string Isbn { get; set; }
    }

    public class DeleteOneEditionCommandValidator : AbstractValidator<DeleteOneEditionCommand>
    {
        public DeleteOneEditionCommandValidator()
        {
            RuleFor(doec => doec.Isbn)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Length(13);
        }
    }
}
