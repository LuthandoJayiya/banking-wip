using log4net;
using MELLBankRestAPI.Models;
using MELLBankRestAPI.Repositories;
using MELLBankRestAPI.ViewModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace MELLBankRestAPI.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class CurrentAccountController : ControllerBase
    {
        private static readonly ILog logger = LogManager.GetLogger("CurrentAccountController");
        private readonly MELLBank_EFDBContext _context;
        CurrentAccountRepository _repository;

        public CurrentAccountController(MELLBank_EFDBContext context)
        {
            _context = context;
            _repository = new CurrentAccountRepository(_context);
        }

        [EnableCors("AllowOrigin")]
        [HttpGet]
        // [Authorize]
        public ActionResult<IEnumerable<CurrentAccountVM>> GetCurrentAccounts()
        {
            try
            {
                logger.Info("Retrieving all current accounts");
                List<CurrentAccountVM> allAcc = _repository.GetAllAccounts();

                return allAcc;
            }
            catch (Exception ex)
            {
                logger.Error($"An error occurred while retrieving all current accounts: {ex.Message}");
                return StatusCode(500);
            }
        }

        [EnableCors("AllowOrigin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<CurrentAccountVM>> GetCurrentAccount(Guid id)
        {
            if (!ModelState.IsValid)
            {
                logger.Error($"An Error has occured, {ModelState}");
                return BadRequest(ModelState);
            }

            try
            {
                var currAccount = await _repository.GetAcountById(id);

                if (currAccount == null)
                {
                    logger.Warn($"Account with ID {id} not found.");
                    return NotFound();
                }
                else
                {
                    logger.Info($"Retrieved Account with ID: {id}");
                    return Ok(currAccount);
                }
            }
            catch (Exception ex)
            {
                logger.Error($"An error occurred while retrieving Account with ID {id}: {ex.Message}");
                return StatusCode(500);
            }
        }

        [EnableCors("AllowOrigin")]
        [HttpGet("AllAccountsForCustomer/{custID}")]
        public async Task<ActionResult<IEnumerable<CurrentAccountVM>>> GetAllAccountsForCustomer(int custID)
        {
            if (!ModelState.IsValid)
            {
                logger.Error($"An Error has occured, {ModelState}");
                return BadRequest(ModelState);
            }

            try
            {
                var currAccounts = await _repository.GetListOfAccountsBelongingToACustomer(custID);

                if (currAccounts == null)
                {
                    logger.Warn($"Account for Customer with {custID} not found.");
                    return NotFound();
                }
                else
                {
                    logger.Info($"Retrieved Account for Customer with ID: {custID}");
                    return Ok(currAccounts);
                }
            }
            catch (Exception ex)
            {
                logger.Error($"An error occurred while retrieving Account for Customer with ID {custID}: {ex.Message}");
                return StatusCode(500);
            }
        }

        [EnableCors("AllowOrigin")]
        [HttpGet("AccountWithCustomerDetails/{accID}")]
        public async Task<ActionResult<IEnumerable<AccountWithCustomerDetailsVM>>> GetAllAccountsForCustomer(Guid accID)
        {
            if (!ModelState.IsValid)
            {
                logger.Error($"An Error has occured, {ModelState}");
                return BadRequest(ModelState);
            }

            try
            {
                var currAccount = await _repository.GetAccountWithCustomerDetails(accID);

                if (currAccount == null)
                {
                    logger.Warn($"Account with {accID} not found.");
                    return NotFound();
                }
                else
                {
                    logger.Info($"Retrieved Account with ID: {accID}");
                    return Ok(currAccount);
                }
            }
            catch (Exception ex)
            {
                logger.Error($"An error occurred while retrieving Account with ID {accID}: {ex.Message}");
                return StatusCode(500);
            }
        }

        [EnableCors("AllowOrigin")]
        [HttpPut("UpdateAccount/{id}")]
        public async Task<IActionResult> PutCurrentAccount(Guid id, CurrentAccountVM accVM)
        {
            logger.Info("PutCurrentAccount - Updating an existing account details.");

            try
            {
                var updatedAcc = await _repository.UpdateAccountDetails(id, accVM);
                logger.Info($"Updated Account with ID: {id}");
                return Ok(new { Message = "Account updated successfully" + updatedAcc });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(id))
                {
                    logger.Warn($"Account with ID {id} not found.");
                    return NotFound("Account was not found");
                }
                else
                {
                    throw;
                }
            }
        }

        [HttpPost]
        [EnableCors("AllowOrigin")]
        //[Authorize]
        public async Task<ActionResult<CurrentAccountVM>> OpenNewAccount(CurrentAccountVM newCurrentAccount, int custId)
        {
            logger.Info("CreateCurrentAccount - Creating a new account.");
            try
            {
               var newAccount = await _repository.OpenNewAccountForCustomer(custId, newCurrentAccount);

                logger.Info($"Created new Account for customer with ID: {custId}");

                return CreatedAtAction("GetCurrentAccount", new { id = newAccount.CurrentId }, newAccount);
            }
            catch (Exception ex)
            {
                logger.Error($"An error occurred while creating a new account: {ex.Message}");
                return StatusCode(500);
            }
        }


        [EnableCors("AllowOrigin")]
        [HttpPut("CloseAccount/{id}")]
        //[Authorize]
        public async Task<ActionResult<CurrentAccountVM>> CloseAccount(Guid id)
        {
            logger.Info("CloseAccount - Closing Account.");
            try
            {

                var account = await _repository.CloseAccountForCustomer(id);

                logger.Info($"Closed Account with ID: {id}");

                return Ok(new { Message = "Account closed successfully" + account });
            }
            catch (Exception ex)
            {
                logger.Error($"An error occurred while closing Account with ID {id}: {ex.Message}");
                return StatusCode(500);
            }
        }

        private bool AccountExists(Guid id)
        {
            return _context.CurrentAccounts.Any(e => e.CurrentId == id);
        }
    }
}
