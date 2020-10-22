using Domain.Models.Sensors;
using MediatR;
using System;

namespace ApplicationServices.Commands.Sensors
{
    public class CreateSensorCommand : IRequest<Sensor>
    {
        public Guid FarmId { get; set; }
        public SensorState State { get; set; }
    }
}
