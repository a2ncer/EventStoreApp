using System;

namespace Domain.Models.Sensors
{
    public class Sensor : DomainModel
    {   public Guid FarmId { get; set; }
        public SensorState State { get; set; }
    }
}
