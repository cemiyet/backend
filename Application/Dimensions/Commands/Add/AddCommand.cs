using MediatR;

namespace Cemiyet.Application.Dimensions.Commands.Add
{
    // TODO (v0.1): create validator.
    public class AddCommand : IRequest
    {
        public double Width { get; set; }
        public double Height { get; set; }
    }
}
