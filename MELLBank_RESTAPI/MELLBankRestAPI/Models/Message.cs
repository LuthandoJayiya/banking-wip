using System;
using System.Collections.Generic;

namespace MELLBankRestAPI.Models
{
    public partial class Message
    {
        public int MessageId { get; set; }
        public string? EmailMessage { get; set; }
        public string? LogMessage { get; set; }
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; } = null!;
    }
}
