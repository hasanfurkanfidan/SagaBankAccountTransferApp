namespace Shared.Events.Interfaces
{
    public interface ITranserStartedEvent
    {
        public string ReceiverIbanNumber { get; set; }
        public string SenderIbanNumber { get; set; }
        public decimal Price { get; set; }
    }
}
