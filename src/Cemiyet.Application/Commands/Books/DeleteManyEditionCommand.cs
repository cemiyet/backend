using FluentValidation;
using MediatR;

namespace Cemiyet.Application.Commands.Books
{
    public class DeleteManyEditionCommand : IRequest
    {
        public string[] Isbns { get; set; }
    }

    public class DeleteManyEditionCommandValidator : AbstractValidator<DeleteManyEditionCommand>
    {
        public DeleteManyEditionCommandValidator()
        {
            RuleFor(dmec => dmec.Isbns).NotNull();
            RuleFor(dmec => dmec.Isbns.Length).GreaterThan(1);
            RuleForEach(dmec => dmec.Isbns).NotEmpty().When(dmec => dmec.Isbns.Length > 1);
        }
    }
}
