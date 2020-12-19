using MediatR;

namespace Cemiyet.Application.Authors.Commands.Add
{
    public class AddCommand : IRequest
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Bio { get; set; }
    }
}
