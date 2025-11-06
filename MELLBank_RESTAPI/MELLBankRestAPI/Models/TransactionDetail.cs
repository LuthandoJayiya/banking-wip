using System;
using System.Collections.Generic;

namespace MELLBankRestAPI.Models
{
    public partial class TransactionDetail
    {
        public int TransDetailId { get; set; }
        public DateTime TransactionDate { get; set; }
        public double Amount { get; set; }
        public string TransactionType { get; set; } = null!;
        public string? Description { get; set; }
        public int TransactionId { get; set; }
    }
}
