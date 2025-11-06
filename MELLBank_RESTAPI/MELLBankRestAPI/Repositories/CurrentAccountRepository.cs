using MELLBankRestAPI.AdapterPattern;
using MELLBankRestAPI.Models;
using MELLBankRestAPI.ViewModels;
using Microsoft.EntityFrameworkCore;
namespace MELLBankRestAPI.Repositories
{
    public class CurrentAccountRepository
    {
        private readonly MELLBank_EFDBContext _context;
        private readonly IModelAdapter<CurrentAccountVM, CurrentAccount> _currentAccountAdapter;

        public CurrentAccountRepository(MELLBank_EFDBContext context)
        {
            _context = context;
            _currentAccountAdapter = new CurrentAccountAdapter();
        }

        public virtual List<CurrentAccountVM> GetAllAccounts()
        {
            List<CurrentAccount> Accounts = _context.CurrentAccounts.ToList();
            var allAccounts = Accounts.Select(acc => _currentAccountAdapter.ConvertModelToViewModel(acc)).ToList();

            return allAccounts;
        }

        public async virtual Task<CurrentAccountVM> GetAcountById(Guid id)
        {
            var Account = await _context.CurrentAccounts.FindAsync(id);
            var AccountVM = _currentAccountAdapter.ConvertModelToViewModel(Account);

            return AccountVM;
        }

        public async virtual Task<List<CurrentAccountVM>> GetListOfAccountsBelongingToACustomer(int custID)
        {
            // Fetch all accounts belonging to the specified customer
            List<CurrentAccount> accountsForCustomer = await _context.CurrentAccounts
                                                                      .Where(c => c.CustomerId == custID)
                                                                      .ToListAsync();

            var accountVMs = accountsForCustomer.Select(acc=>_currentAccountAdapter.ConvertModelToViewModel(acc)).ToList();

            return accountVMs;
        }

        public async virtual Task<AccountWithCustomerDetailsVM> GetAccountWithCustomerDetails(Guid accountID)
        {
            var accountWithCustomer = await (from account in _context.CurrentAccounts
                                             join customer in _context.Customers
                                             on account.CustomerId equals customer.CustomerId
                                             where account.CurrentId == accountID
                                             select new AccountWithCustomerDetailsVM
                                             {
                                                 Account = new CurrentAccountVM
                                                 {
                                                     CurrentId = account.CurrentId,
                                                     CustomerId = account.CustomerId,
                                                     AccountNumber = account.AccountNumber,
                                                     CreationDate = account.CreationDate,
                                                     Balance = account.Balance,
                                                     BranchCode = account.BranchCode,
                                                     OverdraftAmount = account.OverdraftAmount,
                                                     OverdraftRate = account.OverdraftRate,
                                                     CloseDate = account.CloseDate,
                                                 },
                                                 Customer = new CustomerVM
                                                 {
                                                     CustomerId = customer.CustomerId,
                                                     FirstName = customer.FirstName,
                                                     LastName = customer.LastName,
                                                     Username = customer.Username,
                                                     Email = customer.Email,
                                                     PhoneNumber = customer.PhoneNumber,
                                                     StreetAddress = customer.StreetAddress,
                                                     City = customer.City,
                                                     Province = customer.Province,
                                                     PostalCode = customer.PostalCode
                                                 }
                                             }).FirstOrDefaultAsync();

            return accountWithCustomer;
        }

        public async virtual Task<CurrentAccountVM> OpenNewAccountForCustomer(int customerID, CurrentAccountVM newAccount)
        {
            // Check if the customer exists we can't create account for a customer that doesn't exists
            var customer = await _context.Customers.FindAsync(customerID) ?? throw new Exception("Customer not found");

            string accountNumber =GenerateRandom10DigitNumber();
            // Create a new account for the existing customer
            var account = new CurrentAccount
            {
                CurrentId = Guid.NewGuid(),
                CustomerId = customer.CustomerId,
                AccountNumber = accountNumber,
                CreationDate = DateTime.Now,
                Balance = newAccount.Balance,
                BranchCode = newAccount.BranchCode,
                OverdraftAmount = newAccount.OverdraftAmount,
                OverdraftRate = newAccount.OverdraftRate,
                CloseDate = newAccount.CloseDate,
            };

            _context.CurrentAccounts.Add(account);
            await _context.SaveChangesAsync();

            var newAccountVM = _currentAccountAdapter.ConvertModelToViewModel(account);

            return newAccountVM;
        }

        public async virtual Task<CurrentAccountVM> CloseAccountForCustomer(Guid accountID)
        {
            var account = await _context.CurrentAccounts.FindAsync(accountID);
            if (account == null)
            {
                throw new Exception("Account not found");
            }

            account.CloseDate = DateTime.UtcNow;

            _context.CurrentAccounts.Update(account);
            await _context.SaveChangesAsync();

            var closedAccountVM = _currentAccountAdapter.ConvertModelToViewModel(account);

            return closedAccountVM;
        }
        public async virtual Task<CurrentAccountVM> UpdateAccountDetails(Guid accountID, CurrentAccountVM updatedAccountDetails)
        {
            var account = await _context.CurrentAccounts.FindAsync(accountID);
            if (account == null)
            {
                throw new Exception("Account not found");
            }

            // Update the account details only if the account is not closed
            if (account.CloseDate == null)
            {
                account.Balance = updatedAccountDetails.Balance;
                account.CurrentId = accountID;
                account.AccountNumber = account.AccountNumber;
                account.CreationDate = account.CreationDate;
                account.BranchCode = updatedAccountDetails?.BranchCode;
                account.OverdraftAmount = updatedAccountDetails.OverdraftAmount;
                account.OverdraftRate = updatedAccountDetails.OverdraftRate;
                account.CloseDate = account.CloseDate;
                account.CustomerId = account.CustomerId;

                _context.CurrentAccounts.Update(account);
                //_context.Entry(account).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Cannot update details of a closed account");
            }

            // Return the updated account details
            var updatedAccountVM = _currentAccountAdapter.ConvertModelToViewModel(account);

            return updatedAccountVM;

        }

        public string GenerateRandom10DigitNumber()
        {
            Random random = new Random();
            string result = "";

            for (int i = 0; i < 10; i++)
            {
                result += random.Next(0, 10).ToString();
            }

            return result;
        }

    }
}
