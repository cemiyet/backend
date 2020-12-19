using MediatR;

namespace Cemiyet.Application.Books.Commands.DeleteManyEdition
{
    public class DeleteManyEditionCommand : IRequest
    {
        public string[] Isbns { get; set; }
    }
}
