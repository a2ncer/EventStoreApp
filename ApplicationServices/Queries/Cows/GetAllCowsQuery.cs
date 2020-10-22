using Domain.Models.Cows;
using MediatR;
using System.Collections;
using System.Collections.Generic;

namespace ApplicationServices.Queries.Cows
{
    public class GetAllCowsQuery : IRequest<List<Cow>>
    {
    }
}
