using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cemiyet.Application.Series.Commands.DeleteBook
{
    public class DeleteBookCommand : IRequest
    {
        public Guid Id { get; set; }
        public Guid[] BookIds { get; set; }
    }
}
