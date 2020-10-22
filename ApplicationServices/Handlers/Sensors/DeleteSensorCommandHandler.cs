using ApplicationServices.Commands.Sensors;
using Domain.Models.Sensors;
using Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationServices.Handlers.Sensors
{
    public class DeleteSensorCommandHandler : IRequestHandler<DeleteSensorCommand>
    {
        private readonly IEventRepository<Sensor> _eventRepository;
        private readonly ISensorsRepository _snapShotRepository;

        public DeleteSensorCommandHandler(IEventRepository<Sensor> eventRepository, ISensorsRepository SensorsRepository)
        {
            _eventRepository = eventRepository;
            _snapShotRepository = SensorsRepository;
        }
        
        //Idempotent method
        public async Task<Unit> Handle(DeleteSensorCommand request, CancellationToken cancellationToken)
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
