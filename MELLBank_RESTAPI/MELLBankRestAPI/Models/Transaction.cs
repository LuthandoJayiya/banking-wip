using System;
using System.Collections.Generic;

namespace MELLBankRestAPI.Models
{
    public partial class Transaction
    {
        public int TransactionId { get; set; }
        public Guid AccountId { get; set; }
        public int CustomerId { get; set; }
        public string AccountType { get; set; } = null!;
    }
}
