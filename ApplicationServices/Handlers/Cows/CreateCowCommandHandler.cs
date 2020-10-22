using ApplicationServices.Commands.Cows;
using Domain.Models.Cows;
using Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationServices.Handlers.Cows
{
    public class CreateCowCommandHandler : IRequestHandler<CreateCowCommand, Cow>
    {
        private readonly IEventRepository<Cow> _eventRepository;
        private readonly ICowsRepository _snapShotRepository;

        public CreateCowCommandHandler(IEventRepository<Cow> eventRepository, ICowsRepository cowsRepository)
        {
            _eventRepository = eventRepository;
            _snapShotRepository = cowsRepository;
        }
        public async Task<Cow> Handle(CreateCowCommand request, CancellationToken cancellationToken)
        {
            var model = new Cow { FarmId = request.FarmId, State = request.State };
            await _eventRepository.CreateAsync(model);
            await _snapShotRepository.SaveAsync(model);

            return model;
        }
    }
}
