using Shared.Events.Interfaces;
using System;

namespace Shared.Events.Implementations
{
    public class PriceSendedReceiverBankEvent : IPriceSendedReceiverBankEvent
    {
        public PriceSendedReceiverBankEvent(Guid correlationId)
        {
            CorrelationId = correlationId;
        }
        public string ReceiverIbanNumber { get; set; }
        public decimal Price { get; set; }

        public Guid CorrelationId { get; }
    }
}
