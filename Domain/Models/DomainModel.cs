using System;

namespace Domain.Models
{
    public abstract class DomainModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
