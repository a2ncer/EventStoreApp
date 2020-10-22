using ApplicationServices.Queries.Cows;
using Domain.Models.Cows;
using Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationServices.Handlers.Cows
{
    public class GetCowQueryHandler : IRequestHandler<GetCowQuery, Cow>
    {
        private readonly ICowsRepository _snapShotRepository;

        public GetCowQueryHandler(ICowsRepository cowsRepository)
        {
            _snapShotRepository = cowsRepository;
        }

        public Task<Cow> Handle(GetCowQuery request, CancellationToken cancellationToken)
        {
            return _snapShotRepository.GetAsync(request.Id);
        }
    }
}
