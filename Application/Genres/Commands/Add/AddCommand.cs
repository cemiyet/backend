using MediatR;

namespace Cemiyet.Application.Genres.Commands.Add
{
    // TODO (v0.1): create validator.
    public class AddCommand : IRequest
    {
        public string Name { get; set; }
    }
}
