using ApplicationServices.Commands.Sensors;
using Domain.Models.Sensors;
using Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationServices.Handlers.Sensors
{
    public class UpdateSensorCommandHandler : IRequestHandler<UpdateSensorCommand>
    {
        private readonly IEventRepository<Sensor> _eventRepository;
        private readonly ISensorsRepository _snapShotRepository;

        public UpdateSensorCommandHandler(IEventRepository<Sensor> eventRepository, ISensorsRepository SensorsRepository)
        {
            _eventRepository = eventRepository;
            _snapShotRepository = SensorsRepository;
        }

        //Idempotent method
        public async Task<Unit> Handle(UpdateSensorCommand request, CancellationToken cancellationToken)
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
