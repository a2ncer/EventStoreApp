using ApplicationServices.Queries.Cows;
using Domain.Models.Cows;
using Domain.Repositories;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationServices.Handlers.Cows
{
    public class GetCowsCountOnFarmOnDateQueryHandler : IRequestHandler<GetCowsCountOnFarmOnDateQuery, int>
    {
        private readonly IEventRepository<Cow> _eventRepository;

        public GetCowsCountOnFarmOnDateQueryHandler(IEventRepository<Cow> eventRepository)
        {
            _eventRepository = eventRepository;
        }
        public async Task<int> Handle(GetCowsCountOnFarmOnDateQuery request, CancellationToken cancellationToken)
        {
            var projection = await _eventRepository.ProjectAsync(request.Date);
            return projection.Count(x => x.FarmId == request.FarmId && x.State == request.State);
        }
    }
}
