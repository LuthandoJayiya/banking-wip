using log4net;
using MELLBankRestAPI.AuthModels;
using MELLBankRestAPI.Models;
using MELLBankRestAPI.Services;
using MELLBankRestAPI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MELLBankRestAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private static readonly ILog logger = LogManager.GetLogger("ApplicationUserController");

        private readonly MELLBank_EFDBContext _context;

        private readonly IdentityHelper _identityHelper;
        private UserManager<ApplicationUser> _userManager;
        private readonly AuthenticationContext _authContext;
        private readonly RoleManager<IdentityRole> _roleManager;

        public TransactionsController(MELLBank_EFDBContext context, UserManager<ApplicationUser> userManager, AuthenticationContext authContext, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _authContext = authContext;
            _roleManager = roleManager;
            _identityHelper = new IdentityHelper(userManager, authContext, roleManager);
        }


        [EnableCors("AllowOrigin")]
        // POST: api/Transactions/Deposit
        [HttpPost("Deposit")]
        public async Task<IActionResult> Deposit([FromBody] TransactionsVM depositRequest)
        {

            logger.Info("TransactionsController - POST : /gst");
            try
            {
                // Extract accountId from the request
                var accountId = depositRequest.AccountId;

                // Check if the account type is valid (Savings or Current)
                if (depositRequest.AccountType != "Savings" && depositRequest.AccountType != "Current")
                {
                    return BadRequest("Invalid account type. Please select either Savings or Current.");
                }

                // Find the account based on the account type and account ID
                object account;
                int customerId;

                if (depositRequest.AccountType == "Savings")
                {
                    var savingsAccount = await _context.SavingsAccounts.FirstOrDefaultAsync(sa => sa.SavingsId == accountId);
                    if (savingsAccount == null)
                    {
                        return BadRequest("Account not found.");
                    }
                    account = savingsAccount;
                    customerId = savingsAccount.CustomerId; // Retrieve CustomerId from the found SavingsAccount

                    // Check if deposit amount is negative
                    if (depositRequest.Amount < 0)
                    {
                        return BadRequest("Negative deposits are not allowed.");
                    }

                    savingsAccount.Balance += depositRequest.Amount; // Update balance
                }
                else
                {
                    var currentAccount = await _context.CurrentAccounts.FirstOrDefaultAsync(ca => ca.CurrentId == accountId);
                    if (currentAccount == null)
                    {
                        return BadRequest("Account not found.");
                    }
                    account = currentAccount;
                    customerId = currentAccount.CustomerId; // Retrieve CustomerId from the found CurrentAccount

                    // Check if deposit amount is negative
                    if (depositRequest.Amount < 0)
                    {
                        return BadRequest("Negative deposits are not allowed.");
                    }

                    currentAccount.Balance += depositRequest.Amount; // Update balance
                }

                // Create a new transaction record
                var transaction = new Transaction
                {
                    AccountId = accountId,
                    CustomerId = customerId,
                    AccountType = depositRequest.AccountType
                };

                // Save the transaction to the database and get the generated TransactionId
                await _context.Transactions.AddAsync(transaction);
                await _context.SaveChangesAsync();

                // Create a new transaction detail record
                var transactionDetail = new TransactionDetail
                {
                    TransactionDate = DateTime.UtcNow,
                    Amount = depositRequest.Amount,
                    TransactionType = "Deposit",
                    Description = "Deposit to account",
                    TransactionId = transaction.TransactionId
                };

                // Save the transaction detail to the database
                await _context.TransactionDetails.AddAsync(transactionDetail);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Deposit successful." });
            }
            catch (Exception ex)
            {
                // Log the exception (logger implementation is assumed to be available)
                logger.Error("An error occurred during deposit.", ex);
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [EnableCors("AllowOrigin")]
        [HttpPost("Withdraw")]
        public async Task<IActionResult> Withdraw([FromBody] TransactionsVM withdrawalRequest)
        {
            try
            {
                // Check if the account type is valid (Savings or Current)
                if (withdrawalRequest.AccountType != "Savings" && withdrawalRequest.AccountType != "Current")
                {
                    return BadRequest("Invalid account type. Please select either Savings or Current.");
                }

                object account;
                int customerId;

                if (withdrawalRequest.AccountType == "Savings")
                {
                    var savingsAcc = await _context.SavingsAccounts.FirstOrDefaultAsync(sa => sa.SavingsId == withdrawalRequest.AccountId);
                    if (savingsAcc == null)
                    {
                        return BadRequest("Savings account not found.");
                    }

                    account = savingsAcc;
                    customerId = savingsAcc.CustomerId;
                }
                else
                {
                    var currentAcc = await _context.CurrentAccounts.FirstOrDefaultAsync(ca => ca.CurrentId == withdrawalRequest.AccountId);
                    if (currentAcc == null)
                    {
                        return BadRequest("Current account not found.");
                    }

                    account = currentAcc;
                    customerId = currentAcc.CustomerId;
                }

                if (account == null)
                {
                    return BadRequest("Account not found.");
                }

                double amount = withdrawalRequest.Amount;
                if (amount < 0)
                {
                    return BadRequest("Invalid withdrawal amount. Please provide a non-negative amount.");
                }

                if (account is SavingsAccount savingsAccount)
                {
                    if (savingsAccount.Balance < amount)
                    {
                        return BadRequest("Insufficient funds.");
                    }
                    savingsAccount.Balance -= amount;
                }
                else if (account is CurrentAccount currentAccount)
                {
                    if (currentAccount.Balance < amount)
                    {
                        return BadRequest("Insufficient funds.");
                    }
                    currentAccount.Balance -= amount;
                }
                else
                {
                    return BadRequest("Invalid account type.");
                }

                var transaction = new Transaction
                {
                    AccountId = withdrawalRequest.AccountId,
                    CustomerId = customerId,
                    AccountType = withdrawalRequest.AccountType
                };

                await _context.Transactions.AddAsync(transaction);
                await _context.SaveChangesAsync();

                var transactionDetail = new TransactionDetail
                {
                    TransactionDate = DateTime.UtcNow,
                    Amount = -amount,
                    TransactionType = "Withdrawal",
                    Description = "Withdrawal from account",
                    TransactionId = transaction.TransactionId
                };

                await _context.TransactionDetails.AddAsync(transactionDetail);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Withdrawal successful." });
            }
            catch (Exception ex)
            {
                logger.Error("An error occurred during withdrawal.", ex);
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }



        [EnableCors("AllowOrigin")]
        //[HttpGet("transaction/{id}")]
        [HttpGet("TransactionDetails/{id}")]
        public async Task<ActionResult<IEnumerable<TransDetailsVM>>> GetTransDetails(int id)
        {
            try
            {
                var transactionDetails = await _context.TransactionDetails.Where(sa => sa.TransactionId == id).ToListAsync();
                logger.Info($"Successfully retrived all transaction details for transaction  id: {id}");
                return Ok(transactionDetails);
            }
            catch (Exception ex)
            {
                logger.Error($"Unable to retrieve transaction details for transaction id: {id}", ex);
                return StatusCode(500, "Internal server error");
            }
        }

        [EnableCors("AllowOrigin")]
        //[HttpGet("customer/{id}")]
        [HttpGet("Transaction/Customer/{id}")]
        public async Task<ActionResult<IEnumerable<TransactionsVM>>> GetTransactionByCustomerId(int id)
        {
            try
            {
                var transaction = await _context.Transactions.Where(sa => sa.CustomerId == id).ToListAsync();
                logger.Info($"Successfully retrived all transactions for customer  id: {id}");
                return Ok(transaction);
            }
            catch (Exception ex)
            {
                logger.Error($"Unable to retrieve transaction for customer id: {id}", ex);
                return StatusCode(500, "Internal server error");
            }
        }

        [EnableCors("AllowOrigin")]
        [HttpGet("GetTransaction/{id}")]
        public async Task<ActionResult<IEnumerable<TransactionsVM>>> GetTransaction(int id)
        {
            try
            {
                var transactions = await _context.Transactions.Where(sa => sa.CustomerId == id)
                    .Select(t => new TransactionsVM
                    {
                        CustomerId = t.CustomerId,
                        AccountId = t.AccountId,
                        AccountType = t.AccountType,
                        Amount = _context.TransactionDetails
                            .Where(td => td.TransactionId == t.TransactionId)
                            .Sum(td => td.Amount)
                    })
                    .ToListAsync();

                logger.Info($"Successfully retrieved all transactions for customer id: {id}");
                return Ok(transactions);
            }
            catch (Exception ex)
            {
                logger.Error($"Unable to retrieve transactions for customer id: {id}", ex);
                return StatusCode(500, "Internal server error");
            }
        }

        [EnableCors("AllowOrigin")]
        [HttpGet("transactions/c/{id}")]
        public async Task<ActionResult<IEnumerable<TransactionsVM>>> GetTransactions(int id)
        {
            try
            {
                var transactions = await _context.Transactions.Where(sa => sa.CustomerId == id)
                    .Select(t => new TransactionsVM
                    {
                        CustomerId = t.CustomerId,
                        AccountId = t.AccountId,
                        AccountType = t.AccountType,
                        Amount = _context.TransactionDetails
                            .Where(td => td.TransactionId == t.TransactionId)
                            .Sum(td => td.Amount)
                    })
                    .ToListAsync();

                logger.Info($"Successfully retrieved all transactions for customer id: {id}");
                return Ok(transactions);
            }
            catch (Exception ex)
            {
                logger.Error($"Unable to retrieve transactions for customer id: {id}", ex);
                return StatusCode(500, "Internal server error");
            }
        }

        [EnableCors("AllowOrigin")]
        [HttpGet("MyTransactions")]
        public async Task<ActionResult<IEnumerable<TransactionsVM>>> GetMyTransactions()
        {
            try
            {
                string userId = User.Claims.First(c => c.Type == "UserID").Value;
                var user = await _userManager.FindByIdAsync(userId);

                bool userAuthorization = await _identityHelper.IsUserInRole(userId, "Customer");

                if (!userAuthorization)
                {
                    return Unauthorized(new { message = "You need to be logged in to view your transactions" });
                }

                var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Username == user.UserName);
                if (customer is null)
                {
                    return NotFound(new { message = "An error occurred while fetching your transactions" });
                }

                var transactions = await _context.Transactions
                    .Where(t => t.CustomerId == customer.CustomerId)
                    .Select(t => new TransactionsVM
                    {
                        CustomerId = t.CustomerId,
                        AccountId = t.AccountId,
                        AccountType = t.AccountType,
                        Amount = _context.TransactionDetails
                            .Where(td => td.TransactionId == t.TransactionId)
                            .Sum(td => td.Amount)
                    })
                    .ToListAsync();

                return Ok(transactions);
            }
            catch (Exception ex)
            {
                logger.Error($"Error: {ex}");
                return BadRequest(new { message = "An error occurred while trying to fetch transactions", error = ex.Message });
            }
        }




        [EnableCors("AllowOrigin")]
        [HttpGet("MyTransactionDetails")]
        public async Task<ActionResult<IEnumerable<TransDetailsVM>>> GetMyTransactionDetails(int transactionId)
        {
            try
            {
                string userId = User.Claims.First(c => c.Type == "UserID").Value;
                var user = await _userManager.FindByIdAsync(userId);

                bool userAuthorization = await _identityHelper.IsUserInRole(userId, "Customer");

                if (!userAuthorization)
                {
                    return Unauthorized(new { message = "You need to be logged in to view your transaction details" });
                }

                var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Username == user.UserName);
                if (customer is null)
                {
                    return NotFound(new { message = "An error occurred while fetching your transaction details" });
                }

                var transactions = await _context.Transactions
                    .Where(t => t.CustomerId == customer.CustomerId)
                    .ToListAsync();

                if (!transactions.Any(t => t.TransactionId == transactionId))
                {
                    return NotFound(new { message = "Transaction not found for the given transaction ID." });
                }

                var transDetails = await _context.TransactionDetails
                    .Where(t => t.TransactionId == transactionId)
                    .Select(t => new TransDetailsVM
                    {
                        TransDetailId = t.TransDetailId,
                        TransactionDate = t.TransactionDate,
                        Amount = t.Amount,
                        TransactionType = t.TransactionType,
                        Description = t.Description,
                        TransactionId = t.TransactionId
                    })
                    .ToListAsync();

                return Ok(transDetails);
            }
            catch (Exception ex)
            {
                logger.Error($"Error: {ex}");
                return BadRequest(new { message = "An error occurred while trying to fetch transaction details", error = ex.Message });
            }
        }



        //[EnableCors("AllowOrigin")]
        //// POST: api/Transactions/Withdraw
        //[HttpPost("Withdraw")]
        //public async Task<IActionResult> Withdraw([FromBody] TransactionsVM withdrawalRequest)
        //{
        //    try
        //    {
        //        // Extract customerId from the request
        //        var accountId = withdrawalRequest.AccountId;
        //        var customerId = withdrawalRequest.CustomerId;

        //        // Find the customer based on the provided customer ID
        //        var customer = await _context.Customers.FindAsync(customerId);
        //        if (customer == null)
        //        {
        //            return BadRequest("Customer not found.");
        //        }

        //        // Check if the account type is valid (Savings or Current)
        //        if (withdrawalRequest.AccountType != "Savings" && withdrawalRequest.AccountType != "Current")
        //        {
        //            return BadRequest("Invalid account type. Please select either Savings or Current.");
        //        }

        //        // Find the account based on the account type and customer ID
        //        object account;
        //        if (withdrawalRequest.AccountType == "Savings")
        //        {
        //            account = await _context.SavingsAccounts.FirstOrDefaultAsync(sa => sa.CustomerId == customerId && sa.SavingsId == withdrawalRequest.AccountId);
        //        }
        //        else
        //        {
        //            account = await _context.CurrentAccounts.FirstOrDefaultAsync(ca => ca.CustomerId == customerId && ca.CurrentId == withdrawalRequest.AccountId);
        //        }

        //        if (account == null)
        //        {
        //            return BadRequest("Account not found.");
        //        }

        //        // Update the account balance
        //        double amount = withdrawalRequest.Amount;
        //        if (amount < 0)
        //        {
        //            return BadRequest("Invalid withdrawal amount. Please provide a non-negative amount.");
        //        }

        //        if (account is SavingsAccount savingsAccount)
        //        {
        //            if (savingsAccount.Balance < amount)
        //            {
        //                return BadRequest("Insufficient funds.");
        //            }
        //            savingsAccount.Balance -= amount;
        //        }
        //        else if (account is CurrentAccount currentAccount)
        //        {
        //            if (currentAccount.Balance < amount)
        //            {
        //                return BadRequest("Insufficient funds.");
        //            }
        //            currentAccount.Balance -= amount;
        //        }
        //        else
        //        {
        //            return BadRequest("Invalid account type.");
        //        }

        //        // Create a new transaction record
        //        var transaction = new Transaction
        //        {
        //            AccountId = withdrawalRequest.AccountId,
        //            CustomerId = customerId,
        //            AccountType = withdrawalRequest.AccountType
        //        };

        //        // Save the transaction to the database and get the generated TransactionId
        //        await _context.Transactions.AddAsync(transaction);
        //        await _context.SaveChangesAsync();

        //        // Create a new transaction detail record
        //        var transactionDetail = new TransactionDetail
        //        {
        //            TransactionDate = DateTime.UtcNow,
        //            Amount = -amount, // Negative amount to indicate withdrawal
        //            TransactionType = "Withdrawal",
        //            Description = "Withdrawal from account",
        //            TransactionId = transaction.TransactionId
        //        };

        //        // Save the transaction detail to the database
        //        await _context.TransactionDetails.AddAsync(transactionDetail);
        //        await _context.SaveChangesAsync();

        //        return Ok(new { message = "Withdrawal successful." });
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error("An error occurred during withdrawal.", ex);
        //        return StatusCode(500, $"An error occurred: {ex.Message}");
        //    }
        //}

    }
}











