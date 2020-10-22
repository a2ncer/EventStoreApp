using System;

namespace Domain.Models.Cows
{
    public class Cow : DomainModel
    {
        public Guid FarmId { get; set; }
        
        public CowState State { get; set; }
    }
}
