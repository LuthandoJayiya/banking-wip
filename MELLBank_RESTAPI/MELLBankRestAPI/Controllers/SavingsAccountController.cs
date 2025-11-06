using log4net;
using MELLBankRestAPI.AdapterPattern;
using MELLBankRestAPI.Models;
using MELLBankRestAPI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MELLBankRestAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class SavingsAccountController : ControllerBase
    {
        private static readonly ILog logger = LogManager.GetLogger("SavingsAccountController");

        private readonly MELLBank_EFDBContext _context;
        private readonly IModelAdapter<SavingsAccountVM, SavingsAccount> _savingsAccountAdapter;


        public SavingsAccountController(MELLBank_EFDBContext context)
        {
            _context = context;
            _savingsAccountAdapter = new SavingsAccountAdapter();
        }

        [EnableCors("AllowOrigin")]
        [HttpPost]
        public async Task<ActionResult<SavingsAccount>> OpenSavingsAccount(SavingsAccountVM savingsAccountVM)
        {
            var username = HttpContext.User.Identity.Name;
            try
            {
                
                savingsAccountVM.AccountNumber = GenerateAccountNumber();
                savingsAccountVM.CreationDate = DateTime.Now;
                savingsAccountVM.Balance = 0;
                savingsAccountVM.BranchCode = "255774";
                savingsAccountVM.CloseDate = null;
                savingsAccountVM.InterestRateId = 1;

                var newSavAccount = _savingsAccountAdapter.ConvertViewModelToModel(savingsAccountVM);

                _context.SavingsAccounts.Add(newSavAccount);
                await _context.SaveChangesAsync();

                logger.Info($"User: '{username}' created a new savings account with account number: {newSavAccount.AccountNumber} Date: {newSavAccount.CreationDate}");

                return Ok(newSavAccount.SavingsId);
            }
            catch (Exception ex)
            {
                logger.Error($"User: '{username}' was unable to open a new savings account for user", ex);
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Get savings account details.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [EnableCors("AllowOrigin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<SavingsAccount>> GetSavingsAccountById(Guid id)
        {
            var username = HttpContext.User.Identity.Name;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var SavingsAccounts = await _context.SavingsAccounts.FindAsync(id);
            logger.Info($"User '{username}' retrieved details for savings account with id: {id}");

            return Ok(SavingsAccounts);
        }

        /// <summary>
        /// Get all savings accounts for a specific customer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [EnableCors("AllowOrigin")]
        [HttpGet("customer/{id}")]
        public async Task<ActionResult<IEnumerable<SavingsAccount>>> GetSavingsAccounts(int id)
        {
            var username = HttpContext.User.Identity.Name;
            try
            {
                var savingsAccounts = await _context.SavingsAccounts.Where(sa => sa.CustomerId == id).ToListAsync();
                logger.Info($"User: '{username}' successfully retrived all savings accounts for customer id: {id}");
                return Ok(savingsAccounts);
            }
            catch (Exception ex)
            {
                logger.Error($"Unable to retrieve savings accounts for customer id: {id}", ex);
                return StatusCode(500, "Internal server error");
            }
        }

        [EnableCors("AllowOrigin")]       
        [HttpPut("{id}")]
        public async Task<IActionResult> CloseSavingsAccount(Guid id, [FromBody] SavingsAccountVM savingsAccountVM)
        {
            var username = HttpContext.User.Identity.Name;
            try
            {
                var savingsAccount = await _context.SavingsAccounts.FindAsync(id);

                if (savingsAccount == null)
                {
                    return NotFound();
                }

                savingsAccount.CloseDate = DateTime.Now;

                _context.Entry(savingsAccount).State = EntityState.Modified;

                await _context.SaveChangesAsync();
                logger.Info($"User: '{username}' successfully closed savings account with id: {id}");
                return Ok(savingsAccount);
            }
            catch (Exception ex)
            {
                logger.Error("Unable to close savings account", ex);
                return StatusCode(500, "Internal server error");
            }
        }

        [EnableCors("AllowOrigin")]
        [HttpPut("CloseAccount/{accountNumber}")]
        public async Task<IActionResult> CloseSavingsAccount(string accountNumber)
        {
            var username = User.Identity.Name;
            try
            {
                var savingsAccount = await _context.SavingsAccounts.FirstOrDefaultAsync(sa => sa.AccountNumber == accountNumber);

                if (savingsAccount == null)
                {
                    return NotFound();
                }

                savingsAccount.CloseDate = DateTime.Now;

                _context.Entry(savingsAccount).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                logger.Info($"User: '{username}' successfully closed savings account with account number: {accountNumber}");
                return Ok(savingsAccount);
            }
            catch (Exception ex)
            {
                logger.Error("Unable to close savings account", ex);
                return StatusCode(500, "Internal server error");
            }
        }


        private string GenerateAccountNumber()
        {
            return new Random().Next(10000000, 99999999).ToString();
        }

        private bool SavingsAccountExists(Guid id)
        {
            return _context.SavingsAccounts.Any(e => e.SavingsId == id);
        }
    }
}
