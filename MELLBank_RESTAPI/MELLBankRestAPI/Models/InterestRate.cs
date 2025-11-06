using System;
using System.Collections.Generic;

namespace MELLBankRestAPI.Models
{
    public partial class InterestRate
    {
        public InterestRate()
        {
            SavingsAccounts = new HashSet<SavingsAccount>();
        }

        public int InterestRateId { get; set; }
        public double Interest { get; set; }

        public virtual ICollection<SavingsAccount> SavingsAccounts { get; set; }
    }
}
