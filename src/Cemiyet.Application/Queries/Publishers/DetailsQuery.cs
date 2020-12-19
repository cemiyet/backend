using System;
using Cemiyet.Persistence.Application.ViewModels;
using FluentValidation;
using MediatR;

namespace Cemiyet.Application.Queries.Publishers
{
    public class DetailsQuery : IRequest<PublisherViewModel>
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
