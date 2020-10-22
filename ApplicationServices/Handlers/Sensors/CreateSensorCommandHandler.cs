using ApplicationServices.Commands.Sensors;
using Domain.Models.Sensors;
using Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationServices.Handlers.Sensors
{
    public class CreateSensorCommandHandler : IRequestHandler<CreateSensorCommand, Sensor>
    {
        private readonly IEventRepository<Sensor> _eventRepository;
        private readonly ISensorsRepository _snapShotRepository;

        public CreateSensorCommandHandler(IEventRepository<Sensor> eventRepository, ISensorsRepository SensorsRepository)
        {
            _eventRepository = eventRepository;
            _snapShotRepository = SensorsRepository;
        }
        public async Task<Sensor> Handle(CreateSensorCommand request, CancellationToken cancellationToken)
        {
            var model = new Sensor { FarmId = request.FarmId, State = request.State };
            await _eventRepository.CreateAsync(model);
            await _snapShotRepository.SaveAsync(model);

            return model;
        }
    }
}
