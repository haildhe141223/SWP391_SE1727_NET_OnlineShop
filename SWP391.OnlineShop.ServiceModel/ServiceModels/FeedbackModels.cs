using ServiceStack;
using SWP391.OnlineShop.Core.Models.Enums;
using SWP391.OnlineShop.ServiceModel.Results;
using SWP391.OnlineShop.ServiceModel.ViewModels.Feedback;

namespace SWP391.OnlineShop.ServiceModel.ServiceModels
{
    public class FeedbackModels
    {
        [Route("/Feedback/GetAllFeedback", "GET")]
        public class GetAllFeedback : IReturn<List<FeedbackViewModels>>
        {
        }

        [Route("/Feedback/GetFeedbackById", "GET")]
        public class GetFeedbackById : IReturn<FeedbackViewModels>
        {
            public int FeedbackId { get; set; }
        }

        [Route("/Feedback/EditFeedback", "PUT")]
        public class EditFeedback : IReturn<BaseResultModel>
        {
            public int FeedbackId { get; set; }
            public Status Status { get; set; }
        }
    }
}
