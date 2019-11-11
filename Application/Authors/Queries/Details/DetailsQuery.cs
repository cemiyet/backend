using System;
using Cemiyet.Core.Entities;
using MediatR;

namespace Cemiyet.Application.Authors.Queries.Details
{
    public class DetailsQuery : IRequest<Author>
    {
        public Guid Id { get; set; }
    }
}
