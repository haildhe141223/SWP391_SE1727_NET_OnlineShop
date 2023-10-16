using static SWP391.OnlineShop.ServiceModel.ServiceModels.EmailModel;
using SWP391.OnlineShop.ServiceModel.ViewModels.Emails;

namespace SWP391.OnlineShop.ServiceInterface.Interfaces
{
	public interface IEmailService
	{
		Task<EmailViewModel> Post(PostAddEmail request);
	}
}
