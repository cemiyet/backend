using System;
using Cemiyet.Persistence.Application.ViewModels;
using FluentValidation;
using MediatR;

namespace Cemiyet.Application.Queries.Genres
{
    public class DetailsQuery : IRequest<GenreViewModel>
    {
        public Guid Id { get; set; }
    }

    public class DetailsQueryValidator : AbstractValidator<DetailsQuery>
    {
        public DetailsQueryValidator()
        {
            RuleFor(dq => dq.Id).NotEmpty();
        }
    }
}
