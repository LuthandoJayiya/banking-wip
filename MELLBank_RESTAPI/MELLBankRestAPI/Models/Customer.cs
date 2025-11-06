using System;
using System.Collections.Generic;

namespace MELLBankRestAPI.Models
{
    public partial class Customer
    {
        public Customer()
        {
            CurrentAccounts = new HashSet<CurrentAccount>();
            Messages = new HashSet<Message>();
            SavingsAccounts = new HashSet<SavingsAccount>();
        }

        public int CustomerId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? StreetAddress { get; set; }
        public string? City { get; set; }
        public string? Province { get; set; }
        public string? PostalCode { get; set; }

        public virtual ICollection<CurrentAccount> CurrentAccounts { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<SavingsAccount> SavingsAccounts { get; set; }
    }
}
