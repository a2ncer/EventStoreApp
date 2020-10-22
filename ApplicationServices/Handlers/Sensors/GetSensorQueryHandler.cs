using ApplicationServices.Queries.Sensors;
using Domain.Models.Sensors;
using Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationServices.Handlers.Sensors
{
    public class GetSensorQueryHandler : IRequestHandler<GetSensorQuery, Sensor>
    {
        private readonly ISensorsRepository _snapShotRepository;

        public GetSensorQueryHandler(ISensorsRepository SensorsRepository)
        {
            _snapShotRepository = SensorsRepository;
        }

        public Task<Sensor> Handle(GetSensorQuery request, CancellationToken cancellationToken)
        {
            return _snapShotRepository.GetAsync(request.Id);
        }
    }
}
