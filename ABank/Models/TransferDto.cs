namespace ABank.Models
{
    public class TransferDto
    {
        public string ReceiverIbanNumber { get; set; }
        public string SenderIbanNumber { get; set; }
        public decimal Price { get; set; }
    }
}
