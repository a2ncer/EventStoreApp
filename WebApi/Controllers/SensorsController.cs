using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationServices.Commands.Sensors;
using ApplicationServices.Queries.Sensors;
using Domain.Models.Sensors;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SensorsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SensorsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<SensorsController>
        [HttpGet]
        public Task<List<Sensor>> Get()
        {
            return _mediator.Send(new GetAllSensorsQuery());
        }

        // GET api/<SensorsController>/{id}
        [HttpGet("{id}")]
        public Task<Sensor> Get(Guid id)
        {
            return _mediator.Send(new GetSensorQuery { Id = id });
        }

        // GET api/<SensorsController>/avarage
        [HttpGet("avarage")]
        public Task<double> GetAvarage([FromQuery] GetAvarageSensorsCountEveryMonthQuery request)
        {
            return _mediator.Send(request);
        }

        // GET api/<SensorsController>/count
        [HttpGet("count")]
        public Task<int> GetCount([FromQuery] GetSensorsCountOnDateQuery request)
        {
            return _mediator.Send(request);
        }

        // POST api/<SensorsController>
        [HttpPost]
        public Task<Sensor> Post([FromBody] CreateSensorCommand request)
        {
            return _mediator.Send(request);
        }

        // PUT api/<SensorsController>/{id}
        [HttpPut("{id}")]
        public Task Put(Guid id, [FromBody] CreateSensorCommand request)
        {
            var update = new UpdateSensorCommand
            {
                Id = id,
                FarmId = request.FarmId,
                State = request.State
            };

            return _mediator.Send(update);
        }

        // DELETE api/<SensorsController>/{id}
        [HttpDelete("{id}")]
        public Task Delete(Guid id)
        {
            return _mediator.Send(new DeleteSensorCommand { Id = id });
        }
    }
}
