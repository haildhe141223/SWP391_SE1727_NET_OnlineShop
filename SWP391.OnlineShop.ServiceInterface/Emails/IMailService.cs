using SWP391.OnlineShop.Core.Models.Entities;

namespace SWP391.OnlineShop.ServiceInterface.Emails
{
    public interface IMailService
    {
        int Send(Email email);
        Task<int> SendAsync(Email email);
    }
}
