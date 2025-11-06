using MELLBankRestAPI.Models;

namespace MELLBankRestAPI.Services
{
    public interface IMessageService
    {
        Task<bool> SendMessage(Message message);
        Task<bool> SendMessage(Customer customer, Message message);

    }
}
