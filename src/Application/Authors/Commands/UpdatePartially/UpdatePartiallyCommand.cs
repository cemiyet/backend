using System;
using MediatR;

namespace Cemiyet.Application.Authors.Commands.UpdatePartially
{
    public class UpdatePartiallyCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Bio { get; set; }
    }
}
