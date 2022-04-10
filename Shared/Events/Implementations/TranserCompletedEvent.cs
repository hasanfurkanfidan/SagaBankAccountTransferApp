using Shared.Events.Interfaces;
using System;

namespace Shared.Events.Implementations
{
    public class TranserCompletedEvent : ITranserCompletedEvent
    {
        public TranserCompletedEvent(Guid correlationId)
        {
            CorrelationId = correlationId;
        }
        public Guid CorrelationId { get; }
    }
}
