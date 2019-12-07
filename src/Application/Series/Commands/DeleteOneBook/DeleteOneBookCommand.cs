using System;
using MediatR;

namespace Cemiyet.Application.Series.Commands.DeleteOneBook
{
    public class DeleteOneBookCommand : IRequest
    {
        public Guid Id { get; set; }
        public Guid BookId { get; set; }
    }
}
