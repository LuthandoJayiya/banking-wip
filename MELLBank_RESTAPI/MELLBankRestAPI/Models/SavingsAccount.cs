using System;
using System.Collections.Generic;

namespace MELLBankRestAPI.Models
{
    public partial class SavingsAccount
    {
        public Guid SavingsId { get; set; }
        public string? AccountNumber { get; set; }
        public DateTime CreationDate { get; set; }
        public double Balance { get; set; }
        public string? BranchCode { get; set; }
        public DateTime? CloseDate { get; set; }
        public int InterestRateId { get; set; }
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; } = null!;
        public virtual InterestRate InterestRate { get; set; } = null!;
    }
}
