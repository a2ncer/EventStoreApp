using ApplicationServices.Queries.Sensors;
using Domain.Models.Sensors;
using Domain.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationServices.Handlers.Sensors
{
    public class GetAllSensorsQueryHandler : IRequestHandler<GetAllSensorsQuery, List<Sensor>>
    {
        private readonly ISensorsRepository _snapShotRepository;

        public GetAllSensorsQueryHandler(ISensorsRepository SensorsRepository)
        {
            _snapShotRepository = SensorsRepository;
        }
        public Task<List<Sensor>> Handle(GetAllSensorsQuery request, CancellationToken cancellationToken)
        {
            return _snapShotRepository.GetAllAsync();
        }
    }
}
