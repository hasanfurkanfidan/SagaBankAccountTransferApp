namespace Shared.Events.Interfaces
{
    public interface IRewindTransferEvent
    {
        public string SenderIbanNumber { get; set; }
        public decimal Price { get; set; }
    }
}
