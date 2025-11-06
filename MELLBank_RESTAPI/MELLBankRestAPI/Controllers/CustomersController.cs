using log4net;
using MELLBankRestAPI.AdapterPattern;
using MELLBankRestAPI.AuthModels;
using MELLBankRestAPI.Models;
using MELLBankRestAPI.Repositories;
using MELLBankRestAPI.Services;
using MELLBankRestAPI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace MELLBankRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomersController : ControllerBase
    {
        private static readonly ILog logger = LogManager.GetLogger("CustomersController");
        private readonly MELLBank_EFDBContext _context;

        private readonly IdentityHelper _identityHelper;
        private UserManager<ApplicationUser> _userManager;
        private readonly AuthenticationContext _authContext;
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly CustomerAdapter _customerAdapter;
        private readonly IMessageService _messageService;

        private readonly CustomerRepository _customerRepository;

        public CustomersController(MELLBank_EFDBContext dbContext, UserManager<ApplicationUser> userManager, AuthenticationContext authContext, RoleManager<IdentityRole> roleManager)
        {
            _context = dbContext;
            _userManager = userManager;
            _authContext = authContext;
            _roleManager = roleManager;
            _identityHelper = new IdentityHelper(userManager, authContext, roleManager);

            _customerAdapter = new CustomerAdapter();
            _customerRepository = new CustomerRepository(dbContext);
            _messageService = new EmailService(dbContext);
        }

        [EnableCors("AllowOrigin")]
        [HttpGet] //api/Customers
        public async Task<ActionResult<IEnumerable<CustomerVM>>> GetCustomers()
        {
            try
            {
                string userId = User.Claims.First(c => c.Type == "UserID").Value;
                var user = await _userManager.FindByIdAsync(userId);

                bool userAuthorization = await _identityHelper.IsSuperUserRole(userId);

                if (!userAuthorization)
                {
                    return Unauthorized(new { message = "Access Denied. You don't have the necessary permissions." });
                }

                var customers = _customerRepository.GetCustomers();
                return Ok(customers);

                //return Ok(customers);
            }
            catch (Exception ex)
            {
                logger.Error("GET: /api/Customers - " + ex);
                return BadRequest(new { message = "An error occurred while fetching the customers. Please try again later." });
            }
        }

        [EnableCors("AllowOrigin")]
        [HttpGet("{id}")] //api/Customers/{id}
        public async Task<ActionResult<CustomerVM>> GetCustomerById(int id)
        {
            try
            {
                string userId = User.Claims.First(c => c.Type == "UserID").Value;
                var user = await _userManager.FindByIdAsync(userId);

                bool userAuthorization = await _identityHelper.IsSuperUserRole(userId);

                if (!userAuthorization)
                {
                    return Unauthorized(new { message = "Access Denied. You don't have the necessary permissions." });
                }

                var customer = _customerRepository.GetCustomerById(id);

                if (customer == null)
                {
                    return NotFound(new { message = $"Customer with ID {id} not found." });
                }

                return Ok(customer);
            }
            catch (Exception ex)
            {
                logger.Error($"GET: /api/Customers/{id} - " + ex);
                return BadRequest(new { message = "An error occurred while fetching the customer. Please try again later." });
            }
        }


        [EnableCors("AllowOrigin")]
        [HttpPost("register")] //api/Customers
        public async Task<ActionResult<CustomerVM>> RegisterCustomer(RegisterCustomerVM customerVM)
        {
            try
            {
                string userId = User.Claims.First(c => c.Type == "UserID").Value;
                var user = await _userManager.FindByIdAsync(userId);

                bool userAuthorization = await _identityHelper.IsSuperUserRole(userId);
                var userRole = await _identityHelper.GetUserAccessLevelRole(userId);

                if (!userAuthorization)
                {
                    return Unauthorized(new { message = "Access Denied. You don't have the necessary permissions." });
                }

                if (string.IsNullOrEmpty(customerVM.Username) || string.IsNullOrEmpty(customerVM.FirstName) || string.IsNullOrEmpty(customerVM.LastName))
                {
                    return BadRequest(new { message = "Username, FirstName, and LastName are required fields." });
                }

                string password = GeneratePassword(customerVM);

                var applicationUser = new ApplicationUser
                {
                    UserName = customerVM.Username,
                    FirstName = customerVM.FirstName,
                    LastName = customerVM.LastName,
                    Email = customerVM.Email
                };

                var result = await _userManager.CreateAsync(applicationUser, password);

                if (!result.Succeeded)
                {
                    return BadRequest(new { message = "An error occurred while creating the user.", errors = result.Errors });
                }

                var userResult = await _userManager.AddToRoleAsync(applicationUser, "Customer");

                if (!userResult.Succeeded)
                {
                    return BadRequest(new { message = "An error occurred while assigning the role to the user.", errors = userResult.Errors });
                }

                customerVM.CustomerId = 0;
                var customer = _customerAdapter.ConvertViewModelToModel(customerVM);

                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();

                var accountMessage = string.Empty;

                if (customerVM.CreateSavingsAccount && customerVM.CreateCurrentAccount)
                {
                    accountMessage = "savings account and current account";
                }
                else if (customerVM.CreateSavingsAccount)
                {
                    accountMessage = "savings account";
                }
                else if (customerVM.CreateCurrentAccount)
                {
                    accountMessage = "current account";
                }
                else
                {
                    accountMessage = "user account";
                }

                var welcomeMessage = $"Welcome {customer.FirstName} {customer.LastName} to MELLBank. Here are your credentials: Username: {customer.Username}, Password: {password}.";
                var logMessage = $"{userRole}: {user.FirstName} {user.LastName} created the {accountMessage} for Customer {customer.FirstName} {customer.LastName} on {DateTime.UtcNow}.";


                var message = new Message
                {
                    EmailMessage = welcomeMessage,
                    LogMessage = logMessage,
                    CustomerId = customer.CustomerId,
                    MessageId = 0
                };

                //customer.Messages.Add(message);
                _customerRepository.AddMessage(message);
                //await _context.SaveChangesAsync();

                //await _messageService.SendMessage(message);
                logger.Info(message.LogMessage);
                logger.Info($"POST: /api/Customers - {_identityHelper.GetUserAccessLevelRole(userId)} {user.FirstName} created a new customer with id {customer.CustomerId}");
                return CreatedAtAction(nameof(GetCustomerById), new { id = customer.CustomerId }, customer);
            }
            catch (Exception ex)
            {
                logger.Error($"POST: /api/Customers - " + ex);
                return BadRequest(new { message = "An error occurred while creating the customer. Please try again later.", error = ex.Message });
            }
        }

        [EnableCors("AllowOrigin")]
        [HttpPut("{id}")] //api/Customers/{id}
        public async Task<ActionResult<Object>> PutCustomer(int id, CustomerVM customerVM)
        {
            if (id != customerVM.CustomerId)
            {
                return BadRequest(new { message = "The CustomerId in the URL does not match the one in the request body." });
            }

            try
            {
                string userId = User.Claims.First(c => c.Type == "UserID").Value;
                var user = await _userManager.FindByIdAsync(userId);

                bool userAuthorization = await _identityHelper.IsSuperUserRole(userId);

                if (!userAuthorization)
                {
                    return Unauthorized(new { message = "Access Denied. You don't have the necessary permissions." });
                }

                var existingCustomer = await _context.Customers.FindAsync(id);
                if (existingCustomer == null)
                {
                    return NotFound(new { message = $"Customer with ID {id} not found." });
                }

                UpdateCustomerFields(customerVM, existingCustomer);

                _context.Entry(existingCustomer).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok(new { message = "Updated successfully" });
            }
            catch (Exception ex)
            {
                logger.Error($"PUT: /api/Customers/{id} - " + ex);
                return BadRequest(new { message = "An error occurred while updating the customer. Please try again later.", error = ex.Message });
            }
        }

        [EnableCors("AllowOrigin")]
        [HttpDelete("{id}")] //api/Customers/{id}
        public async Task<ActionResult<Customer>> DeleteCustomer(int id)
        {
            try
            {
                string userId = User.Claims.First(c => c.Type == "UserID").Value;
                var user = await _userManager.FindByIdAsync(userId);

                bool userAuthorization = await _identityHelper.IsSuperUserRole(userId);

                if (!userAuthorization)
                {
                    return Unauthorized(new { message = "Access Denied. You don't have the necessary permissions." });
                }

                var customer = await _context.Customers.FindAsync(id);
                if (customer == null)
                {
                    return NotFound(new { message = $"Customer with ID {id} not found." });
                }

                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();

                logger.Info($"DELETE: /api/Customers/{id} - {user.FirstName} {user.LastName} deleted a customer with ID {id}");
                return Ok(new { message = "Customer deleted successfully" });
            }
            catch (Exception ex)
            {
                logger.Error($"DELETE: /api/Customers/{id} - " + ex);
                return BadRequest(new { message = "An error occurred while deleting the customer. Please try again later.", error = ex.Message });
            }
        }

        [EnableCors("AllowOrigin")]
        [HttpGet("MyAccounts")]
        public async Task<IActionResult> GetAllAccounts()
        {
            try
            {
                string userId = User.Claims.First(c => c.Type == "UserID").Value;
                var user = await _userManager.FindByIdAsync(userId);

                bool userAuthorization = await _identityHelper.IsUserInRole(userId, "Customer");
                if (!userAuthorization)
                {
                    logger.Warn($"Unauthorized access attempt by user {userId}.");
                    return Unauthorized(new { message = "You need to be logged in to view your accounts" });
                }

                var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Username == user.UserName);
                if (customer == null)
                {
                    logger.Warn($"Customer not found for user {userId}.");
                    return NotFound(new { message = "An error occurred while fetching your accounts" });
                }

                var allAccounts = _customerRepository.AllMyAccounts(customer.CustomerId);

                logger.Info($"Accounts successfully retrieved for customer {customer.CustomerId}.");
                return Ok(allAccounts);
            }
            catch (Exception ex)
            {
                logger.Error($"GET: /api/Customers/allAccounts - " + ex);
                return BadRequest(new { message = "An error occurred while fetching your accounts. Please try again later." });
            }
        }

        [EnableCors("AllowOrigin")]
        [HttpGet("MyAccount/{accountId}")]
        public async Task<IActionResult> GetAccountById(Guid accountId)
        {
            try
            {
                string userId = User.Claims.First(c => c.Type == "UserID").Value;
                var user = await _userManager.FindByIdAsync(userId);

                bool userAuthorization = await _identityHelper.IsUserInRole(userId, "Customer");
                if (!userAuthorization)
                {
                    logger.Warn($"Unauthorized access attempt by user {userId}.");
                    return Unauthorized(new { message = "You need to be logged in to view your account details" });
                }

                var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Username == user.UserName);
                if (customer == null)
                {
                    logger.Warn($"Customer not found for user {userId}.");
                    return NotFound(new { message = "An error occurred while fetching your account details" });
                }

                var account = _customerRepository.GetAccountById(customer.CustomerId, accountId);
                if (account == null)
                {
                    logger.Warn($"Account with ID {accountId} not found for customer {customer.CustomerId}.");
                    return NotFound(new { message = $"Account with ID {accountId} is not found. Please review your entry." });
                }

                logger.Info($"Account with ID {accountId} successfully retrieved for customer {customer.CustomerId}.");
                return Ok(account);
            }
            catch (Exception ex)
            {
                logger.Error($"GET: /api/Customers/MyAccount/{accountId} - " + ex);
                return BadRequest(new { message = "An error occurred while fetching your account details. Please try again later.", error = ex.Message });
            }
        }


        private void UpdateCustomerFields(CustomerVM customerVM, Customer existingCustomer)
        {
            existingCustomer.FirstName = customerVM.FirstName ?? string.Empty;
            existingCustomer.LastName = customerVM.LastName ?? string.Empty;
            existingCustomer.Email = customerVM.Email;
            existingCustomer.PhoneNumber = customerVM.PhoneNumber;
            existingCustomer.StreetAddress = customerVM.StreetAddress;
            existingCustomer.City = customerVM.City;
            existingCustomer.Province = customerVM.Province;
            existingCustomer.PostalCode = customerVM.PostalCode;
        }

        private string GeneratePassword(RegisterCustomerVM customer)
        {
            var password = new StringBuilder();

            if (customer.Username?.Length > 0)
            {
                password.Append(char.ToUpper(customer.Username[0]));
                password.Append(
                    customer.Username.Length > 1 ? char.ToLower(customer.Username[1]) : char.ToLower(customer.Username[1]));
            }

            password.Append(new Random().Next(0, 10));

            List<char> specialCharacters = new List<char> { '!', '@', '#', '$', '%', '^', '&', '*' };
            password.Append(specialCharacters[new Random().Next(0, specialCharacters.Count)]);

            while (password.Length < 6)
            {
                password.Append((char)new Random().Next(97, 123));
                password.Append((char)new Random().Next(65, 91));
            }

            return password.ToString();
        }
    }
}