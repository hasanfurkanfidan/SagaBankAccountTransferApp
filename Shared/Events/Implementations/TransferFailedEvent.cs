using Shared.Events.Interfaces;
using System;

namespace Shared.Events.Implementations
{
    public class TransferFailedEvent : ITransferFailedEvent
    {
        public TransferFailedEvent(Guid correlationId)
        {
            CorrelationId = correlationId;
        }
        public Guid CorrelationId { get; }
    }
}
