namespace MELLBankRestAPI.ViewModels
{
    public class AccountDetailVM
    {
        public Guid AccountId { get; set; }
        public string AccountNumber { get; set; }
        public DateTime CreationDate { get; set; }
        public double Balance { get; set; }
        public string BranchCode { get; set; }
        public string AccountType { get; set; }
        public DateTime? CloseDate { get; set; }

        public int TotalNumberOfTransactions { get; set; }
        public double? OverdraftAmount { get; set; }
        public double? OverdraftRate { get; set; }
        public int? InterestRateId { get; set; }
    }
}
