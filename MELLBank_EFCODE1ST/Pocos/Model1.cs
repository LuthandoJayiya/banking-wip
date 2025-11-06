using Pocos.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Pocos
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>()
                .HasKey(td => new { td.TransactionId, td.AccountId, td.CustomerId });

            modelBuilder.Entity<SavingsAccount>().Property(sa => sa.SavingsId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            
            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<InterestRate> InterestRates { get; set; }
        public virtual DbSet<SavingsAccount> SavingsAccounts { get; set; }
        public virtual DbSet<CurrentAccount> CurrentAccounts { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<TransactionDetail> TransactionDetails { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
    }
}
