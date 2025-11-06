using MELLBankRestAPI.Models;

namespace MELLBankRestAPI.Services
{

    public class EmailService : IMessageService
    {
        private readonly MELLBank_EFDBContext _context;

        public EmailService(MELLBank_EFDBContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<bool> SendMessage(Customer customer, Message message)
        {
            try
            {
                customer.Messages.Add(message);
                var entries = await _context.SaveChangesAsync();

                return entries > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> SendMessage(Message message)
        {
            try
            {
                var customer = await _context.Customers.FindAsync(message.CustomerId);

                if (customer == null)
                {
                    return false;
                }
                await _context.Messages.AddAsync(message);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
