using Shared.Events.Interfaces;

namespace Shared.Events.Implementations
{
    public class TransferStartedEvent : ITranserStartedEvent
    {
        public string ReceiverIbanNumber { get; set; }
        public string SenderIbanNumber { get; set; }
        public decimal Price { get; set; }
    }
}
