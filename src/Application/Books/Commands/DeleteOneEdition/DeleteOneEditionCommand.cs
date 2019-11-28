using MediatR;

namespace Cemiyet.Application.Books.Commands.DeleteOneEdition
{
    public class DeleteOneEditionCommand : IRequest
    {
        public string Isbn { get; set; }
    }
}
