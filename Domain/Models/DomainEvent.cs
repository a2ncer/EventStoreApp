using System;

namespace Domain.Models
{
    public class DomainEvent<T>
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public T Model { get; set; }
        public EventType EventType { get; set; }
        public DateTimeOffset OccuredAt { get; set; } = DateTimeOffset.UtcNow;
    }
}
