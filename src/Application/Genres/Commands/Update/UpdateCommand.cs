using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cemiyet.Application.Genres.Commands.Update
{
    public class UpdateCommand : IRequest
    {
        [FromRoute]
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
