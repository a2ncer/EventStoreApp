using ApplicationServices.Queries.Sensors;
using Domain.Models.Sensors;
using Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationServices.Handlers.Sensors
{
    public class GetAvarageSensorsCountEveryMonthQueryHandler : IRequestHandler<GetAvarageSensorsCountEveryMonthQuery, double>
    {
        private readonly IEventRepository<Sensor> _eventRepository;

        public GetAvarageSensorsCountEveryMonthQueryHandler(IEventRepository<Sensor> eventRepository)
        {
            _eventRepository = eventRepository;
        }
        public async Task<double> Handle(GetAvarageSensorsCountEveryMonthQuery request, CancellationToken cancellationToken)
        {
            var result = new List<int>();
            var range = Enumerable.Range(1, 12).Select(m => new DateTimeOffset(request.Year, m, DateTime.DaysInMonth(request.Year, m), 23, 59, 59, TimeSpan.Zero));
            foreach (var date in range)
            {
                var projection = await _eventRepository.ProjectAsync(date);
                result.Add(projection.Count(x => x.State == request.State));
            }
            return result.Average();
        }
    }
}
