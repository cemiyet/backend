using System;
using FluentValidation;
using MediatR;

namespace Cemiyet.Application.Commands.Genres
{
    public class DeleteManyCommand : IRequest
    {
        public Guid[] Ids { get; set; }
    }

    public class DeleteManyCommandValidator : AbstractValidator<DeleteManyCommand>
    {
        public DeleteManyCommandValidator()
        {
            RuleFor(dmc => dmc.Ids).NotNull();
            RuleFor(dmc => dmc.Ids.Length).GreaterThan(1);
            RuleForEach(dmc => dmc.Ids).NotEmpty().When(dmc => dmc.Ids.Length > 1);
        }
    }
}
