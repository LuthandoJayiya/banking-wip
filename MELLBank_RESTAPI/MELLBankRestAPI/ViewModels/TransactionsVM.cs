namespace MELLBankRestAPI.ViewModels
{
    public class TransactionsVM
    {
        public int CustomerId { get; set; }
        public Guid AccountId { get; set; }
        
        public string AccountType { get; set; }
        public double Amount { get; set; }
    }
}
