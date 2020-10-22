using ApplicationServices.Queries.Cows;
using Domain.Models.Cows;
using Domain.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationServices.Handlers.Cows
{
    public class GetAllCowsQueryHandler : IRequestHandler<GetAllCowsQuery, List<Cow>>
    {
        private readonly ICowsRepository _snapShotRepository;

        public GetAllCowsQueryHandler(ICowsRepository cowsRepository)
        {
            _snapShotRepository = cowsRepository;
        }
        public Task<List<Cow>> Handle(GetAllCowsQuery request, CancellationToken cancellationToken)
        {
            return _snapShotRepository.GetAllAsync();
        }
    }
}
