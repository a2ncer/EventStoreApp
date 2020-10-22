using Domain.Models.Cows;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationServices.Queries.Cows
{
    public class GetCowQuery : IRequest<Cow>
    {
        public Guid Id { get; set; }
    }
}
