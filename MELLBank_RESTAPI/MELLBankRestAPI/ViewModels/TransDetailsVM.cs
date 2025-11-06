namespace MELLBankRestAPI.ViewModels
{
    public class TransDetailsVM
    {
       //public string CustomerId { get; set; }
        public int TransDetailId { get; set; }
        public DateTime TransactionDate { get; set; }
        public double Amount { get; set; }
        public string TransactionType { get; set; } = null!;
        public string? Description { get; set; }
        public int TransactionId { get; set; }
    }
}
