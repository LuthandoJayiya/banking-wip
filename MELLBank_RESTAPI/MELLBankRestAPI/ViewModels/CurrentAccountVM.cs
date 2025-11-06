namespace MELLBankRestAPI.ViewModels
{
    public class CurrentAccountVM
    {
        public Guid CurrentId { get; set; }
        public string? AccountNumber { get; set; }
        public DateTime CreationDate { get; set; }
        public double Balance { get; set; }
        public string? BranchCode { get; set; }
        public double OverdraftAmount { get; set; }
        public double OverdraftRate { get; set; }
        public DateTime? CloseDate { get; set; }
        public int CustomerId { get; set; }
    }
}
