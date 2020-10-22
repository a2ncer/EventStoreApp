using Domain.Models.Sensors;
using MediatR;
using System;

namespace ApplicationServices.Queries.Sensors
{
    public class GetSensorQuery : IRequest<Sensor>
    {
        public Guid Id { get; set; }
    }
}
