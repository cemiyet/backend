using System;
using MediatR;

namespace Cemiyet.Application.Dimensions.Commands.DeleteOne
{
    public class DeleteOneCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
