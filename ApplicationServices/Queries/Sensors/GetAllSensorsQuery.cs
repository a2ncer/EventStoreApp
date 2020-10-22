using Domain.Models.Sensors;
using MediatR;
using System.Collections.Generic;

namespace ApplicationServices.Queries.Sensors
{
    public class GetAllSensorsQuery : IRequest<List<Sensor>>
    {
    }
}
