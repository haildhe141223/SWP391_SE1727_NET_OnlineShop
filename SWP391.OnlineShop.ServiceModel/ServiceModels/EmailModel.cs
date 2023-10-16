using ServiceStack;
using SWP391.OnlineShop.ServiceModel.ViewModels.Emails;

namespace SWP391.OnlineShop.ServiceModel.ServiceModels
{
	public class EmailModel
	{
		[Route("/Email/PostAddEmail", "POST")]
		public class PostAddEmail : IReturn<EmailViewModel>
		{
			public string? To { get; set; }
			public string? Subject { get; set; }
			public string? Body { get; set; }
			public string? Category { get; set; }
			public string? Title { get; set; }
		}
	}
}
