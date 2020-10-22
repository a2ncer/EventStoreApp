using Domain.Models.Sensors;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationServices.Queries.Sensors
{
    public class GetSensorsCountOnDateQuery : IRequest<int>
    {
        public DateTime Date { get; set; }
        public SensorState State { get; set; }
    }
}
