using MELLBankRestAPI.Models;

namespace MELLBankRestAPI.ViewModels
{
    public class AccountVM
    {
            public Guid AccountId { get; set; }
            public string AccountNumber { get; set; }
            public DateTime CreationDate { get; set; }
            public double Balance { get; set; }
            public string BranchCode { get; set; }
            public string AccountType { get; set; }
    }
}
