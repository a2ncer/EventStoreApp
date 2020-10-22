using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationServices.Commands.Cows;
using ApplicationServices.Queries.Cows;
using Domain.Models.Cows;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CowsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CowsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<CowsController>
        [HttpGet]
        public Task<List<Cow>> Get()
        {
            return _mediator.Send(new GetAllCowsQuery());
        }

        // GET api/<CowsController>/{id}
        [HttpGet("{id}")]
        public Task<Cow> Get(Guid id)
        {
            return _mediator.Send(new GetCowQuery { Id = id });
        }

        // GET api/<CowsController>/count
        [HttpGet("count")]
        public Task<int> GetCount([FromQuery] GetCowsCountOnFarmOnDateQuery request)
        {
            return _mediator.Send(request);
        }

        // POST api/<CowsController>
        [HttpPost]
        public Task<Cow> Post([FromBody] CreateCowCommand request)
        {
            return _mediator.Send(request);
        }

        // PUT api/<CowsController>/{id}
        [HttpPut("{id}")]
        public Task Put(Guid id, [FromBody] CreateCowCommand request)
        {
            var update = new UpdateCowCommand
            {
                Id = id,
                FarmId = request.FarmId,
                State = request.State
            };

            return _mediator.Send(update);
        }

        // DELETE api/<CowsController>/{id}
        [HttpDelete("{id}")]
        public Task Delete(Guid id)
        {
            return _mediator.Send(new DeleteCowCommand { Id = id });
        }
    }
}
