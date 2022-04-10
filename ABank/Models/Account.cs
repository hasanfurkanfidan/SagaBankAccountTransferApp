namespace ABank.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string IbanNumber { get; set; }
        public decimal Balance { get; set; }
    }
}
