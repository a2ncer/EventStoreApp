using Domain.Models.Cows;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationServices.Commands.Cows
{
    public class UpdateCowCommand : IRequest
    {
        public Guid Id { get; set; }
        public Guid FarmId { get; set; }
        public CowState State { get; set; }
    }
}
