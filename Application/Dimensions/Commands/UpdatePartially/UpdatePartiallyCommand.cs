using System;
using MediatR;

namespace Cemiyet.Application.Dimensions.Commands.UpdatePartially
{
    public class UpdatePartiallyCommand : IRequest
    {
        public Guid Id { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
    }
}
