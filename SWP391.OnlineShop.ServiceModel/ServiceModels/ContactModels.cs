using ServiceStack;
using SWP391.OnlineShop.ServiceModel.Results;
using SWP391.OnlineShop.ServiceModel.ViewModels.Emails;

namespace SWP391.OnlineShop.ServiceModel.ServiceModels
{
    public class ContactModels
    {
        [Route("/Contact/PostAddContact", "POST")]
        public class PostAddContact : IReturn<BaseResultModel>
        {
            public string Name { get; set; }
            public string Email { get; set; }
            public string Subject { get; set; }
            public string Message { get; set; }
        }

        [Route("/Contact/GetContacts", "GET")]
        public class GetContacts : IReturn<List<EmailContactViewModel>>
        {
            public string Category { get; set; }
        }

        [Route("/Contact/GetContact", "GET")]
        public class GetContact : IReturn<EmailContactViewModel>
        {
            public int Id { get; set; }
        }
    }
}
