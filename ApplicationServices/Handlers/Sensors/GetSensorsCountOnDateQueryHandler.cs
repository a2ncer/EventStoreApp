using ApplicationServices.Queries.Sensors;
using Domain.Models.Sensors;
using Domain.Repositories;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationServices.Handlers.Sensors
{
    public class GetSensorsCountOnDateQueryHandler : IRequestHandler<GetSensorsCountOnDateQuery, int>
    {
        private readonly IEventRepository<Sensor> _eventRepository;

        public GetSensorsCountOnDateQueryHandler(IEventRepository<Sensor> eventRepository)
        {
            _eventRepository = eventRepository;
        }
        public async Task<int> Handle(GetSensorsCountOnDateQuery request, CancellationToken cancellationToken)
        {
            var projection = await _eventRepository.ProjectAsync(request.Date);
            return projection.Count(x => x.State == request.State);
        }
    }
}
