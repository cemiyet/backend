using MediatR;

namespace Cemiyet.Application.Publishers.Commands.Add
{
    public class AddCommand : IRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
