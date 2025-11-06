namespace Pocos.Migrations
{
    using Pocos.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Pocos.Model1>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Pocos.Model1 context)
        {
            //Customers
            context.Customers.AddOrUpdate(c => c.CustomerId,
                new Customer() { CustomerId = 1, FirstName = "Jabu", LastName = "Nkosi", Username = "JabuNkosi", Email = "jabu.nkosi@gmail.com", PhoneNumber = "0829876543", StreetAddress = "32 Kgosi Mampuru Street", City = "Pretoria", Province = "Gauteng", PostalCode = "0002" },
                new Customer() { CustomerId = 2, FirstName = "Lebo", LastName = "Mokoena", Username = "LeboMokoena", Email = "lebo.mokoena@gmail.com", PhoneNumber = "0836549871", StreetAddress = "45 Rubida Street", City = "Bloemfontein", Province = "Free State", PostalCode = "9301" },
                new Customer() { CustomerId = 3, FirstName = "Mpho", LastName = "Khumalo", Username = "MphoKhumalo", Email = "mpho.ngwenya@gmail.com", PhoneNumber = "0824567890", StreetAddress = "23 Kloof Street", City = "Cape Town", Province = "Western Cape", PostalCode = "8001" },
                new Customer() { CustomerId = 4, FirstName = "Sizwe", LastName = "Dlamini", Username = "SizweDlamini", Email = "sizwe.dlamini@gmail.com", PhoneNumber = "0839876543", StreetAddress = "67 Westville Road", City = "Durban", Province = "KwaZulu-Natal", PostalCode = "4001" },
                new Customer() { CustomerId = 5, FirstName = "Nomsa", LastName = "Mkhize", Username = "NomsaMkhize", Email = "nomsa.mkhize@gmail.com", PhoneNumber = "0821234567", StreetAddress = "14 Kruger Street", City = "Polokwane", Province = "Limpopo", PostalCode = "0700" },
                new Customer() { CustomerId = 6, FirstName = "Mandla", LastName = "Zulu", Username = "MandlaZulu", Email = "mandla.zulu@gmail.com", PhoneNumber = "0837654321", StreetAddress = "89 Nelson Mandela Boulevard", City = "Port Elizabeth", Province = "Eastern Cape", PostalCode = "6001" },
                new Customer() { CustomerId = 7, FirstName = "Thando", LastName = "Maseko", Username = "ThandoMaseko", Email = "thando.maseko@gmail.com", PhoneNumber = "0827654321", StreetAddress = "12 Vilakazi Street", City = "Soweto", Province = "Gauteng", PostalCode = "1804" },
                new Customer() { CustomerId = 8, FirstName = "Siyabonga", LastName = "Nkosi", Username = "SiyabongaNkosi", Email = "siyabonga.nkosi@gmail.com", PhoneNumber = "0824567890", StreetAddress = "67 Albertina Sisulu Road", City = "Kimberley", Province = "Northern Cape", PostalCode = "8301" },
                new Customer() { CustomerId = 9, FirstName = "Thuli", LastName = "Mthembu", Username = "ThuliMthembu", Email = "thuli.mthembu@gmail.com", PhoneNumber = "0839876543", StreetAddress = "22 Church Street", City = "Nelspruit", Province = "Mpumalanga", PostalCode = "1200" },
                new Customer() { CustomerId = 10, FirstName = "Sibongile", LastName = "Khumalo", Username = "SibongileKhumalo", Email = "sibongile.khumalo@gmail.com", PhoneNumber = "0821234567", StreetAddress = "54 Main Road", City = "Upington", Province = "Northern Cape", PostalCode = "8800" }
            );

            // //Interest Rate
            context.InterestRates.AddOrUpdate(c => c.InterestRateId,
                 new InterestRate() { InterestRateId = 1, Interest = 14.5 }
           );

            // //Savings Account

            context.SavingsAccounts.AddOrUpdate(c => c.AccountNumber,
               new SavingsAccount() { CustomerId = 1, AccountNumber = "87567745", CreationDate = DateTime.Now, Balance = 1000.50, BranchCode = "255774", InterestRateId = 1, CloseDate = null },
               new SavingsAccount() { CustomerId = 2, AccountNumber = "12085644", CreationDate = DateTime.Now, Balance = 1500.75, BranchCode = "255774", InterestRateId = 1, CloseDate = null },
               new SavingsAccount() { CustomerId = 3, AccountNumber = "37457664", CreationDate = DateTime.Now, Balance = 2000.00, BranchCode = "255774", InterestRateId = 1, CloseDate = null },
               new SavingsAccount() { CustomerId = 4, AccountNumber = "75755466", CreationDate = DateTime.Now.AddDays(-100), Balance = 2500.25, BranchCode = "255774", InterestRateId = 1, CloseDate = DateTime.Now.AddDays(-50) },
               new SavingsAccount() { CustomerId = 5, AccountNumber = "99786554", CreationDate = DateTime.Now, CloseDate = DateTime.Now.AddDays(10), Balance = 3000.50, BranchCode = "255774", InterestRateId = 1 },
               new SavingsAccount() { CustomerId = 6, AccountNumber = "22468776", CreationDate = DateTime.Now, Balance = 3500.75, BranchCode = "255774", InterestRateId = 1, CloseDate = null },
               new SavingsAccount() { CustomerId = 7, AccountNumber = "22997564", CreationDate = DateTime.Now, Balance = 4000.00, BranchCode = "255774", InterestRateId = 1, CloseDate = null },
               new SavingsAccount() { CustomerId = 8, AccountNumber = "22656474", CreationDate = DateTime.Now, Balance = 4500.25, BranchCode = "255774", InterestRateId = 1, CloseDate = null },
               new SavingsAccount() { CustomerId = 9, AccountNumber = "77453553", CreationDate = DateTime.Now, Balance = 5000.50, BranchCode = "255774", InterestRateId = 1, CloseDate = null },
               new SavingsAccount() { CustomerId = 10, AccountNumber = "22375463", CreationDate = DateTime.Now, Balance = 5500.75, BranchCode = "255774", InterestRateId = 1, CloseDate = null }
           );

            // //Current Account

            context.CurrentAccounts.AddOrUpdate(c => c.AccountNumber,
                new CurrentAccount { CustomerId = 1, AccountNumber = "1234567890", Balance = 1000.00, CreationDate = DateTime.Now, BranchCode = "255774", OverdraftAmount = 500.00, OverdraftRate = 14.5, CloseDate = null, },
                new CurrentAccount { CustomerId = 2, AccountNumber = "2345678901", Balance = 1500.00, CreationDate = DateTime.Now, BranchCode = "255774", OverdraftAmount = 600.00, OverdraftRate = 14.5, CloseDate = null, },
                new CurrentAccount { CustomerId = 3, AccountNumber = "3456789012", Balance = 2000.00, CreationDate = DateTime.Now, BranchCode = "255774", OverdraftAmount = 700.00, OverdraftRate = 14.5, CloseDate = null, },
                new CurrentAccount { CustomerId = 4, AccountNumber = "4567890123", Balance = 2500.00, CreationDate = DateTime.Now, BranchCode = "255774", OverdraftAmount = 800.00, OverdraftRate = 14.5, CloseDate = null, },
                new CurrentAccount { CustomerId = 5, AccountNumber = "5678901234", Balance = 3000.00, CreationDate = DateTime.Now.AddDays(-100), BranchCode = "255774", OverdraftAmount = 900.00, OverdraftRate = 14.5, CloseDate = DateTime.Now.AddDays(-50) },
                new CurrentAccount { CustomerId = 6, AccountNumber = "6789012345", Balance = 3500.00, CreationDate = DateTime.Now, BranchCode = "255774", OverdraftAmount = 1000.00, OverdraftRate = 14.5, CloseDate = null },
                new CurrentAccount { CustomerId = 7, AccountNumber = "7890123456", Balance = 4000.00, CreationDate = DateTime.Now, BranchCode = "255774", OverdraftAmount = 1100.00, OverdraftRate = 14.5, CloseDate = null },
                new CurrentAccount { CustomerId = 8, AccountNumber = "8901234567", Balance = 4500.00, CreationDate = DateTime.Now, BranchCode = "255774", OverdraftAmount = 1200.00, OverdraftRate = 14.5, CloseDate = null },
                new CurrentAccount { CustomerId = 9, AccountNumber = "9012345678", Balance = 5000.00, CreationDate = DateTime.Now, BranchCode = "255774", OverdraftAmount = 1300.00, OverdraftRate = 14.5, CloseDate = null },
                new CurrentAccount { CustomerId = 10, AccountNumber = "0123456789", Balance = 5500.00, CreationDate = DateTime.Now, BranchCode = "255774", OverdraftAmount = 1400.00, OverdraftRate = 14.5, CloseDate = null }
            );

            context.SaveChanges();
            
            var transactions = new List<Transaction>();

            foreach (var savingsAccount in context.SavingsAccounts.ToList())
            {
                transactions.Add(new Transaction
                {
                    AccountId = savingsAccount.SavingsId,
                    AccountType = "Savings",
                    CustomerId = savingsAccount.CustomerId
                });
            }

            foreach (var currentAccount in context.CurrentAccounts.ToList())
            {
                transactions.Add(new Transaction
                {
                    AccountId = currentAccount.CurrentId,
                    AccountType = "Current",
                    CustomerId = currentAccount.CustomerId
                });
            }
            context.Transactions.AddOrUpdate(transactions.ToArray());

            context.SaveChanges();


            // //Transaction Details
            context.TransactionDetails.AddOrUpdate(
                new TransactionDetail { TransDetailId = 1, TransactionId = 1, TransactionDate = DateTime.Now.AddDays(0), Amount = 1000, TransactionType = "Deposit", Description = "Online shopping" },
                new TransactionDetail { TransDetailId = 2, TransactionId = 2, TransactionDate = DateTime.Now.AddDays(-9), Amount = 1200, TransactionType = "Withdrawal", Description = "Grocery shopping" },
                new TransactionDetail { TransDetailId = 3, TransactionId = 3, TransactionDate = DateTime.Now.AddDays(-8), Amount = 1300, TransactionType = "Deposit", Description = "Rent payment" },
                new TransactionDetail { TransDetailId = 4, TransactionId = 4, TransactionDate = DateTime.Now.AddDays(-7), Amount = 2700, TransactionType = "Deposit", Description = "Salary" },
                new TransactionDetail { TransDetailId = 5, TransactionId = 5, TransactionDate = DateTime.Now.AddDays(-6), Amount = 700, TransactionType = "Withdrawal", Description = "Electricity bill" },
                new TransactionDetail { TransDetailId = 6, TransactionId = 6, TransactionDate = DateTime.Now.AddDays(-5), Amount = 3000, TransactionType = "Deposit", Description = "Loan payment" },
                new TransactionDetail { TransDetailId = 7, TransactionId = 7, TransactionDate = DateTime.Now.AddDays(-4), Amount = 14000, TransactionType = "Deposit", Description = "Freelance work" },
                new TransactionDetail { TransDetailId = 8, TransactionId = 8, TransactionDate = DateTime.Now.AddDays(-3), Amount = 1500, TransactionType = "Withdrawal", Description = "Car repair" },
                new TransactionDetail { TransDetailId = 9, TransactionId = 9, TransactionDate = DateTime.Now.AddDays(-2), Amount = 1000, TransactionType = "Deposit", Description = "Gift to friend" },
                new TransactionDetail { TransDetailId = 10, TransactionId = 10, TransactionDate = DateTime.Now.AddDays(-3), Amount = 500, TransactionType = "Deposit", Description = "Stockvel" },
                new TransactionDetail { TransDetailId = 11, TransactionId = 11, TransactionDate = DateTime.Now.AddDays(-2), Amount = 500, TransactionType = "Withdrawal", Description = "shopping" },
                new TransactionDetail { TransDetailId = 12, TransactionId = 12, TransactionDate = DateTime.Now.AddDays(-1), Amount = 700, TransactionType = "Withdrawal", Description = "Food payment" },
                new TransactionDetail { TransDetailId = 13, TransactionId = 13, TransactionDate = DateTime.Now.AddDays(-4), Amount = 800, TransactionType = "Withdrawal", Description = "Shoes payment" },
                new TransactionDetail { TransDetailId = 14, TransactionId = 14, TransactionDate = DateTime.Now.AddDays(-5), Amount = 900, TransactionType = "Deposit", Description = "allowance" },
                new TransactionDetail { TransDetailId = 15, TransactionId = 15, TransactionDate = DateTime.Now.AddDays(0), Amount = 800, TransactionType = "Deposit", Description = "tents rental" },
                new TransactionDetail { TransDetailId = 16, TransactionId = 16, TransactionDate = DateTime.Now.AddDays(-2), Amount = 500, TransactionType = "Withdrawal", Description = "Salon payment" },
                new TransactionDetail { TransDetailId = 17, TransactionId = 17, TransactionDate = DateTime.Now.AddDays(-3), Amount = 600, TransactionType = "Deposit", Description = "Freelance work" },
                new TransactionDetail { TransDetailId = 18, TransactionId = 18, TransactionDate = DateTime.Now.AddDays(-3), Amount = 800, TransactionType = "Deposit", Description = "Food payment" },
                new TransactionDetail { TransDetailId = 19, TransactionId = 19, TransactionDate = DateTime.Now.AddDays(-3), Amount = 500, TransactionType = "Deposit", Description = "Salon payment" },
                new TransactionDetail { TransDetailId = 20, TransactionId = 20, TransactionDate = DateTime.Now.AddDays(-3), Amount = 800, TransactionType = "Deposit", Description = "Freelance work" }
            );

            // // Messages
            context.Messages.AddOrUpdate(m => m.MessageId,
                new Message() { MessageId = 1, EmailMessage = "Welcome Thando Maseko to MELLBank, your current account has been created. Here are your credentials Username: ThandoMaseko, Password: z*EH5x*h4A.", LogMessage = "Sindi Bank Staff created the current account for Customer Thando Maseko on 2024-06-05.", CustomerId = 1 },
                new Message() { MessageId = 2, EmailMessage = "Welcome Lebo Mokoena to MELLBank, your savings account has been created. Here are your credentials Username: LeboMokoena, Password: z*EH5x*h4A.", LogMessage = "Sipho Admin created the savings account for Customer Lebo Mokoena on 2024-06-05.", CustomerId = 2 },
                new Message() { MessageId = 3, EmailMessage = "Welcome Mpho Khumalo to MELLBank, your current account has been created. Here are your credentials Username: MphoKhumalo, Password: z*EH5x*h4A.", LogMessage = "Sindi Bank Staff created the current account for Customer Mpho Khumalo on 2024-06-05.", CustomerId = 3 },
                new Message() { MessageId = 4, EmailMessage = "Welcome Sizwe Dlamini to MELLBank, your savings account has been created. Here are your credentials Username: SizweDlamini, Password: z*EH5x*h4A.", LogMessage = "Sipho Admin created the savings account for Customer Sizwe Dlamini on 2024-06-05.", CustomerId = 4 },
                new Message() { MessageId = 5, EmailMessage = "Welcome Nomsa Mkhize to MELLBank, your current account has been created. Here are your credentials Username: NomsaMkhize, Password: z*EH5x*h4A.", LogMessage = "Sindi Bank Staff created the current account for Customer Nomsa Mkhize on 2024-06-05.", CustomerId = 5 },
                new Message() { MessageId = 6, EmailMessage = "Welcome Mandla Zulu to MELLBank, your savings account has been created. Here are your credentials Username: MandlaZulu, Password: z*EH5x*h4A.", LogMessage = "Sipho Admin created the savings account for Customer Mandla Zulu on 2024-06-05.", CustomerId = 6 },
                new Message() { MessageId = 7, EmailMessage = "Welcome Lerato Molefe to MELLBank, your current account has been created. Here are your credentials Username: LeratoMolefe, Password: z*EH5x*h4A.", LogMessage = "Sindi Bank Staff created the current account for Customer Lerato Molefe on 2024-06-05.", CustomerId = 7 },
                new Message() { MessageId = 8, EmailMessage = "Welcome Siyabonga Nkosi to MELLBank, your savings account has been created. Here are your credentials Username: SiyabongaNkosi, Password: z*EH5x*h4A.", LogMessage = "Sipho Admin created the savings account for Customer Siyabonga Nkosi on 2024-06-05.", CustomerId = 8 },
                new Message() { MessageId = 9, EmailMessage = "Welcome Thuli Mthembu to MELLBank, your current account has been created. Here are your credentials Username: ThuliMthembu, Password: z*EH5x*h4A.", LogMessage = "Sindi Bank Staff created the current account for Customer Thuli Mthembu on 2024-06-05.", CustomerId = 9 },
                new Message() { MessageId = 10, EmailMessage = "Welcome Sibongile Khumalo to MELLBank, your savings account has been created. Here are your credentials Username: SibongileKhumalo, Password: z*EH5x*h4A.", LogMessage = "Sipho Admin created the savings account for Customer Sibongile Khumalo on 2024-06-05.", CustomerId = 10 },
                new Message() { MessageId = 11, EmailMessage = "Welcome Thando Maseko to MELLBank, your savings account has been created. Here are your credentials Username: ThandoMaseko, Password: z*EH5x*h4A.", LogMessage = "Sipho Admin created the savings account for Customer Thando Maseko on 2024-06-05.", CustomerId = 1 },
                new Message() { MessageId = 12, EmailMessage = "Welcome Lebo Mokoena to MELLBank, your current account has been created. Here are your credentials Username: LeboMokoena, Password: z*EH5x*h4A.", LogMessage = "Sindi Bank Staff created the current account for Customer Lebo Mokoena on 2024-06-05.", CustomerId = 2 },
                new Message() { MessageId = 13, EmailMessage = "Welcome Mpho Khumalo to MELLBank, your savings account has been created. Here are your credentials Username: MphoKhumalo, Password: z*EH5x*h4A.", LogMessage = "Sipho Admin created the savings account for Customer Mpho Khumalo on 2024-06-05.", CustomerId = 3 },
                new Message() { MessageId = 14, EmailMessage = "Welcome Sizwe Dlamini to MELLBank, your current account has been created. Here are your credentials Username: SizweDlamini, Password: z*EH5x*h4A.", LogMessage = "Sindi Bank Staff created the current account for Customer Sizwe Dlamini on 2024-06-05.", CustomerId = 4 },
                new Message() { MessageId = 15, EmailMessage = "Welcome Nomsa Mkhize to MELLBank, your savings account has been created. Here are your credentials Username: NomsaMkhize, Password: z*EH5x*h4A.", LogMessage = "Sipho Admin created the savings account for Customer Nomsa Mkhize on 2024-06-05.", CustomerId = 5 },
                new Message() { MessageId = 16, EmailMessage = "Welcome Mandla Zulu to MELLBank, your current account has been created. Here are your credentials Username: MandlaZulu, Password: z*EH5x*h4A.", LogMessage = "Sindi Bank Staff created the current account for Customer Mandla Zulu on 2024-06-05.", CustomerId = 6 },
                new Message() { MessageId = 17, EmailMessage = "Welcome Lerato Molefe to MELLBank, your savings account has been created. Here are your credentials Username: LeratoMolefe, Password: z*EH5x*h4A.", LogMessage = "Sipho Admin created the savings account for Customer Lerato Molefe on 2024-06-05.", CustomerId = 7 },
                new Message() { MessageId = 18, EmailMessage = "Welcome Siyabonga Nkosi to MELLBank, your current account has been created. Here are your credentials Username: SiyabongaNkosi, Password: z*EH5x*h4A.", LogMessage = "Sindi Bank Staff created the current account for Customer Siyabonga Nkosi on 2024-06-05.", CustomerId = 8 },
                new Message() { MessageId = 19, EmailMessage = "Welcome Thuli Mthembu to MELLBank, your savings account has been created. Here are your credentials Username: ThuliMthembu, Password: z*EH5x*h4A.", LogMessage = "Sipho Admin created the savings account for Customer Thuli Mthembu on 2024-06-05.", CustomerId = 9 },
                new Message() { MessageId = 20, EmailMessage = "Welcome Sibongile Khumalo to MELLBank, your current account has been created. Here are your credentials Username: SibongileKhumalo, Password: z*EH5x*h4A.", LogMessage = "Sindi Bank Staff created the current account for Customer Sibongile Khumalo on 2024-06-05.", CustomerId = 10 }
            );

            context.SaveChanges();
        }
    }
}
