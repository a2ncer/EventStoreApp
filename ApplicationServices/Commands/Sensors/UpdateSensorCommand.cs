using Domain.Models.Sensors;
using MediatR;
using System;

namespace ApplicationServices.Commands.Sensors
{
    public class UpdateSensorCommand : IRequest
    {
        public Guid Id { get; set; }
        public Guid FarmId { get; set; }
        public SensorState State { get; set; }
    }
}
