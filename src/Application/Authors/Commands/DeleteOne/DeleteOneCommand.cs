using System;
using MediatR;

namespace Cemiyet.Application.Authors.Commands.DeleteOne
{
    public class DeleteOneCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
