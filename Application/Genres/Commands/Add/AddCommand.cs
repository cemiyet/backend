using MediatR;

namespace Cemiyet.Application.Genres.Commands.Add
{
    public class AddCommand : IRequest
    {
        public string Name { get; set; }
    }
}
