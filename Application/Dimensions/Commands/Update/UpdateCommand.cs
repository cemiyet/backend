using System;
using MediatR;

namespace Cemiyet.Application.Dimensions.Commands.Update
{
    // TODO (v0.1): create validator.
    public class UpdateCommand : IRequest
    {
        public Guid Id { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
    }
}
