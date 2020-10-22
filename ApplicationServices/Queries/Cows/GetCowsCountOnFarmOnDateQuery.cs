using Domain.Models.Cows;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationServices.Queries.Cows
{
    public class GetCowsCountOnFarmOnDateQuery : IRequest<int>
    {
        public Guid FarmId { get; set; }
        public DateTimeOffset Date { get; set; }
        public CowState State { get; set; }
    }
}
