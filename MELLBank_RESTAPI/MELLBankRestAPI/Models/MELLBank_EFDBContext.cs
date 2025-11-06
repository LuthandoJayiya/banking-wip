using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MELLBankRestAPI.Models
{
    public partial class MELLBank_EFDBContext : DbContext
    {
        public MELLBank_EFDBContext()
        {
        }

        public MELLBank_EFDBContext(DbContextOptions<MELLBank_EFDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CurrentAccount> CurrentAccounts { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<InterestRate> InterestRates { get; set; } = null!;
        public virtual DbSet<Message> Messages { get; set; } = null!;
        public virtual DbSet<MigrationHistory> MigrationHistories { get; set; } = null!;
        public virtual DbSet<SavingsAccount> SavingsAccounts { get; set; } = null!;
        public virtual DbSet<Transaction> Transactions { get; set; } = null!;
        public virtual DbSet<TransactionDetail> TransactionDetails { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CurrentAccount>(entity =>
            {
                entity.HasKey(e => e.CurrentId)
                    .HasName("PK_dbo.current_accounts");

                entity.ToTable("current_accounts");

                entity.HasIndex(e => e.CustomerId, "IX_customer_id");

                entity.Property(e => e.CurrentId)
                    .HasColumnName("current_id")
                    .HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.AccountNumber).HasColumnName("account_number");

                entity.Property(e => e.Balance).HasColumnName("balance");

                entity.Property(e => e.BranchCode).HasColumnName("branch_code");

                entity.Property(e => e.CloseDate)
                    .HasColumnType("datetime")
                    .HasColumnName("close_date");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("creation_date");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.OverdraftAmount).HasColumnName("overdraft_amount");

                entity.Property(e => e.OverdraftRate).HasColumnName("overdraft_rate");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CurrentAccounts)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_dbo.current_accounts_dbo.customers_customer_id");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customers");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.City)
                    .HasMaxLength(100)
                    .HasColumnName("city");

                entity.Property(e => e.Email).HasColumnName("email");

                entity.Property(e => e.FirstName).HasColumnName("first_name");

                entity.Property(e => e.LastName).HasColumnName("last_name");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(20)
                    .HasColumnName("phone_number");

                entity.Property(e => e.PostalCode).HasColumnName("postal_code");

                entity.Property(e => e.Province)
                    .HasMaxLength(100)
                    .HasColumnName("province");

                entity.Property(e => e.StreetAddress)
                    .HasMaxLength(100)
                    .HasColumnName("street_address");

                entity.Property(e => e.Username).HasColumnName("username");
            });

            modelBuilder.Entity<InterestRate>(entity =>
            {
                entity.ToTable("interest_rates");

                entity.Property(e => e.InterestRateId).HasColumnName("interest_rate_id");

                entity.Property(e => e.Interest).HasColumnName("interest");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("messages");

                entity.HasIndex(e => e.CustomerId, "IX_customer_id");

                entity.Property(e => e.MessageId).HasColumnName("message_id");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.EmailMessage).HasColumnName("email_message");

                entity.Property(e => e.LogMessage).HasColumnName("log_message");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_dbo.messages_dbo.customers_customer_id");
            });

            modelBuilder.Entity<MigrationHistory>(entity =>
            {
                entity.HasKey(e => new { e.MigrationId, e.ContextKey })
                    .HasName("PK_dbo.__MigrationHistory");

                entity.ToTable("__MigrationHistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ContextKey).HasMaxLength(300);

                entity.Property(e => e.ProductVersion).HasMaxLength(32);
            });

            modelBuilder.Entity<SavingsAccount>(entity =>
            {
                entity.HasKey(e => e.SavingsId)
                    .HasName("PK_dbo.savings_accounts");

                entity.ToTable("savings_accounts");

                entity.HasIndex(e => e.CustomerId, "IX_customer_id");

                entity.HasIndex(e => e.InterestRateId, "IX_interest_rate_id");

                entity.Property(e => e.SavingsId)
                    .HasColumnName("savings_id")
                    .HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.AccountNumber).HasColumnName("account_number");

                entity.Property(e => e.Balance).HasColumnName("balance");

                entity.Property(e => e.BranchCode).HasColumnName("branch_code");

                entity.Property(e => e.CloseDate)
                    .HasColumnType("datetime")
                    .HasColumnName("close_date");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("creation_date");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.InterestRateId).HasColumnName("interest_rate_id");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.SavingsAccounts)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_dbo.savings_accounts_dbo.customers_customer_id");

                entity.HasOne(d => d.InterestRate)
                    .WithMany(p => p.SavingsAccounts)
                    .HasForeignKey(d => d.InterestRateId)
                    .HasConstraintName("FK_dbo.savings_accounts_dbo.interest_rates_interest_rate_id");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasKey(e => new { e.TransactionId, e.AccountId, e.CustomerId })
                    .HasName("PK_dbo.transactions");

                entity.ToTable("transactions");

                entity.Property(e => e.TransactionId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("transaction_id");

                entity.Property(e => e.AccountId).HasColumnName("account_id");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.AccountType).HasColumnName("account_type");
            });

            modelBuilder.Entity<TransactionDetail>(entity =>
            {
                entity.HasKey(e => e.TransDetailId)
                    .HasName("PK_dbo.transaction_details");

                entity.ToTable("transaction_details");

                entity.Property(e => e.TransDetailId).HasColumnName("trans_detail_id");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.TransactionDate)
                    .HasColumnType("datetime")
                    .HasColumnName("transaction_date");

                entity.Property(e => e.TransactionId).HasColumnName("transaction_id");

                entity.Property(e => e.TransactionType).HasColumnName("transaction_type");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
