using Domain.Models.Sensors;
using MediatR;

namespace ApplicationServices.Queries.Sensors
{
    public class GetAvarageSensorsCountEveryMonthQuery : IRequest<double>
    {
        public SensorState State { get; set; }
        public int Year { get; set; }
    }
}
