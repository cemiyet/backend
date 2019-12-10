using System;
using MediatR;

namespace Cemiyet.Application.Series.Commands.DeleteOne
{
    public class DeleteOneCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
