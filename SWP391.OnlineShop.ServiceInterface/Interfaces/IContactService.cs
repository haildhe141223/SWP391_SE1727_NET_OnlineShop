using SWP391.OnlineShop.ServiceModel.Results;
using SWP391.OnlineShop.ServiceModel.ViewModels.Emails;
using static SWP391.OnlineShop.ServiceModel.ServiceModels.ContactModels;

namespace SWP391.OnlineShop.ServiceInterface.Interfaces;

public interface IContactService
{
    Task<BaseResultModel> Post(PostAddContact request);
    Task<List<EmailContactViewModel>> Get(GetContacts request);
    Task<EmailContactViewModel> Get(GetContact request);
}