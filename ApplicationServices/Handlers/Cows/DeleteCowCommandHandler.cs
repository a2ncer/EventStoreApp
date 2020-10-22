using ApplicationServices.Commands.Cows;
using Domain.Models.Cows;
using Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationServices.Handlers.Cows
{
    public class DeleteCowCommandHandler : IRequestHandler<DeleteCowCommand>
    {
        private readonly IEventRepository<Cow> _eventRepository;
        private readonly ICowsRepository _snapShotRepository;

        public DeleteCowCommandHandler(IEventRepository<Cow> eventRepository, ICowsRepository cowsRepository)
        {
            _eventRepository = eventRepository;
            _snapShotRepository = cowsRepository;
        }
        
        //Idempotent method
        public async Task<Unit> Handle(DeleteCowCommand request, CancellationToken cancellationToken)
        {
            var model = await _snapShotRepository.DeleteAsync(request.Id);
            if (model != default)
            {
                await _eventRepository.DeleteAsync(model);
            }
            return Unit.Value;
        }
    }
}
