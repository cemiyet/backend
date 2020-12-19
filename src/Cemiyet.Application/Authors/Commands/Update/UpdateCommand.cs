using System;
using MediatR;

namespace Cemiyet.Application.Authors.Commands.Update
{
    public class UpdateCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Bio { get; set; }
    }
}
