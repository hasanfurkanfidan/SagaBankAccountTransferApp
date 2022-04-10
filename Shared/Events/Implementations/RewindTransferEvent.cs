using Shared.Events.Interfaces;

namespace Shared.Events.Implementations
{
    public class RewindTransferEvent : IRewindTransferEvent
    {
        public string SenderIbanNumber { get; set; }
        public decimal Price { get; set; }
    }
}
