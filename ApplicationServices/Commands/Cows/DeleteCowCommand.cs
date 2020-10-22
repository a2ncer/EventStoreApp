using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationServices.Commands.Cows
{
    public class DeleteCowCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
