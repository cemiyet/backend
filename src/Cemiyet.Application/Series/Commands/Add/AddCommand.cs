using MediatR;

namespace Cemiyet.Application.Series.Commands.Add
{
    public class AddCommand : IRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
