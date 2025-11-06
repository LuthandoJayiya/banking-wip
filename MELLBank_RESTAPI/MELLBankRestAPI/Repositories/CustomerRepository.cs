using MELLBankRestAPI.AdapterPattern;
using MELLBankRestAPI.Models;
using MELLBankRestAPI.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace MELLBankRestAPI.Repositories
{
    public class CustomerRepository
    {
        private readonly MELLBank_EFDBContext _context;
        private readonly IModelAdapter<CustomerVM, Customer> _customerAdapter;

        public CustomerRepository(MELLBank_EFDBContext context)
        {
            _context = context;
            _customerAdapter = new CustomerAdapter();
        }

        public List<CustomerVM> GetCustomers()
        {
            return _context.Customers.Select(customer => _customerAdapter.ConvertModelToViewModel(customer)).ToList();
        }

        public CustomerVM? GetCustomerById(int customerId)
        {
            var customerQuery = (from customer in _context.Customers
                                 where customer.CustomerId == customerId
                                 select new
                                 {
                                     customer,
                                     CurrentAccounts = _context.CurrentAccounts.Where(ca => ca.CustomerId == customerId).ToList(),
                                     SavingsAccounts = _context.SavingsAccounts.Where(sa => sa.CustomerId == customerId).ToList()
                                 }).FirstOrDefault();

            if (customerQuery == null)
            {
                return null;
            }

            var customerVM = _customerAdapter.ConvertModelToViewModel(customerQuery.customer);

            customerVM.CurrentAccounts = customerQuery.CurrentAccounts.Select(ca => new CurrentAccountVM
            {
                CurrentId = ca.CurrentId,
                AccountNumber = ca.AccountNumber,
                Balance = ca.Balance,
                CreationDate = ca.CreationDate,
                BranchCode = ca.BranchCode,
                OverdraftAmount = ca.OverdraftAmount,
                OverdraftRate = ca.OverdraftRate
            }).ToList();

            customerVM.SavingsAccounts = customerQuery.SavingsAccounts.Select(sa => new SavingsAccountVM
            {
                SavingsId = sa.SavingsId,
                AccountNumber = sa.AccountNumber,
                Balance = sa.Balance,
                CreationDate = sa.CreationDate,
                BranchCode = sa.BranchCode
            }).ToList();

            return customerVM;
        }

        public void CreateCustomer(CustomerVM customerVM)
        {
            var customer = _customerAdapter.ConvertViewModelToModel(customerVM);
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }

        public void UpdateCustomer(CustomerVM customerVM)
        {
            var customer = _customerAdapter.ConvertViewModelToModel(customerVM);
            _context.Customers.Update(customer);
            _context.SaveChanges();
        }

        public void DeleteCustomer(int customerId)
        {
            var customer = _context.Customers.Find(customerId);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                _context.SaveChanges();
            }
        }

        public void AddMessage(Message message)
        {
            _context.Messages.Add(message);
            _context.SaveChanges();
        }


        public List<AccountVM> AllMyAccounts(int customerId)
        {
            var accountsQuery = (from customer in _context.Customers
                                 where customer.CustomerId == customerId
                                 select new
                                 {
                                     CurrentAccounts = _context.CurrentAccounts
                                        .Where(ca => ca.CustomerId == customerId)
                                        .Select(ca => new CurrentAccountVM
                                        {
                                            CurrentId = ca.CurrentId,
                                            AccountNumber = ca.AccountNumber,
                                            Balance = ca.Balance,
                                            CreationDate = ca.CreationDate,
                                            BranchCode = ca.BranchCode,
                                            OverdraftAmount = ca.OverdraftAmount,
                                            OverdraftRate = ca.OverdraftRate,
                                        }).ToList(),
                                     SavingsAccounts = _context.SavingsAccounts
                                        .Where(sa => sa.CustomerId == customerId)
                                        .Select(sa => new SavingsAccountVM
                                        {
                                            SavingsId = sa.SavingsId,
                                            AccountNumber = sa.AccountNumber,
                                            Balance = sa.Balance,
                                            CreationDate = sa.CreationDate,
                                            BranchCode = sa.BranchCode,
                                            InterestRateId = 1,
                                        }).ToList()
                                 }).FirstOrDefault();

            if (accountsQuery == null)
            {
                return new List<AccountVM>();
            }

            var allAccounts = new List<AccountVM>();

            allAccounts.AddRange(accountsQuery.CurrentAccounts.Select(ca => new AccountVM
            {
                AccountId = ca.CurrentId,
                AccountNumber = ca.AccountNumber,
                Balance = ca.Balance,
                CreationDate = ca.CreationDate,
                BranchCode = ca.BranchCode,
                AccountType = "Current"
            }));

            allAccounts.AddRange(accountsQuery.SavingsAccounts.Select(sa => new AccountVM
            {
                AccountId = sa.SavingsId,
                AccountNumber = sa.AccountNumber,
                Balance = sa.Balance,
                CreationDate = sa.CreationDate,
                BranchCode = sa.BranchCode,
                AccountType = "Savings"
            }));

            return allAccounts;
        }
        public AccountDetailVM? GetAccountById(int customerId, Guid accountId)
        {
            var currentAccount = _context.CurrentAccounts
                .Where(ca => ca.CustomerId == customerId && ca.CurrentId == accountId)
                .Select(ca => new AccountDetailVM
                {
                    AccountId = ca.CurrentId,
                    AccountNumber = ca.AccountNumber,
                    CreationDate = ca.CreationDate,
                    Balance = ca.Balance,
                    BranchCode = ca.BranchCode,
                    CloseDate = ca.CloseDate,
                    AccountType = "Current",
                    OverdraftAmount = ca.OverdraftAmount,
                    OverdraftRate = ca.OverdraftRate,
                    TotalNumberOfTransactions = _context.Transactions.Count(t => t.AccountId == ca.CurrentId && t.AccountType == "Current")
                })
                .FirstOrDefault();

            if (currentAccount != null)
            {
                return currentAccount;
            }

            var savingsAccount = _context.SavingsAccounts
                .Where(sa => sa.CustomerId == customerId && sa.SavingsId == accountId)
                .Select(sa => new AccountDetailVM
                {
                    AccountId = sa.SavingsId,
                    AccountNumber = sa.AccountNumber,
                    CreationDate = sa.CreationDate,
                    Balance = sa.Balance,
                    BranchCode = sa.BranchCode,
                    CloseDate = sa.CloseDate,
                    AccountType = "Savings",
                    InterestRateId = sa.InterestRateId,
                    TotalNumberOfTransactions = _context.Transactions.Count(t => t.AccountId == sa.SavingsId && t.AccountType == "Savings")
                })
                .FirstOrDefault();

            return savingsAccount;
        }

    }
}