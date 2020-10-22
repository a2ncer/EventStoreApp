using MediatR;
using System;

namespace ApplicationServices.Commands.Sensors
{
    public class DeleteSensorCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
