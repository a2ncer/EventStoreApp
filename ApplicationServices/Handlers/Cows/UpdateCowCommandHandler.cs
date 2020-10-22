using ApplicationServices.Commands.Cows;
using Domain.Models.Cows;
using Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationServices.Handlers.Cows
{
    public class UpdateCowCommandHandler : IRequestHandler<UpdateCowCommand>
    {
        private readonly IEventRepository<Cow> _eventRepository;
        private readonly ICowsRepository _snapShotRepository;

        public UpdateCowCommandHandler(IEventRepository<Cow> eventRepository, ICowsRepository cowsRepository)
        {
            _eventRepository = eventRepository;
            _snapShotRepository = cowsRepository;
        }

        //Idempotent method
        public async Task<Unit> Handle(UpdateCowCommand request, CancellationToken cancellationToken)
        {
            var model = await _snapShotRepository.GetAsync(request.Id);
            if (model == default || (model.FarmId == request.FarmId && model.State == request.State))
            {
                return Unit.Value;
            }
            model.FarmId = request.FarmId;
            model.State = request.State;

            await _snapShotRepository.SaveAsync(model);
            await _eventRepository.UpdateAsync(model);

            return Unit.Value;
        }
    }
}
