using Domain.Models.Cows;
using MediatR;
using System;

namespace ApplicationServices.Commands.Cows
{
    public class CreateCowCommand : IRequest<Cow>
    {
        public Guid FarmId { get; set; }
        public CowState State { get; set; }
    }
}
