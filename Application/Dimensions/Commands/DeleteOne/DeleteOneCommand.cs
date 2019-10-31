using System;
using MediatR;

namespace Cemiyet.Application.Dimensions.Commands.DeleteOne
{
    // TODO (v0.1): create validator.
    public class DeleteOneCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
