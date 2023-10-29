using SWP391.OnlineShop.ServiceModel.ViewModels.Feedback;
using static SWP391.OnlineShop.ServiceModel.ServiceModels.FeedbackModels;

namespace SWP391.OnlineShop.ServiceInterface.Interfaces
{
    public interface IFeedbackService
    {
        /// <summary>
		/// Get all feedbacks
		/// </summary>
		/// <param name="request"></param>
		/// <returns>feedbacks</returns>
		List<FeedbackViewModels> Get(GetAllFeedback request);

        /// <summary>
        /// Get feedback by Id
        /// </summary>
        /// <param name="request"></param>
        /// <returns>feedbacks</returns>
        FeedbackViewModels Get(GetFeedbackById request);

        /// <summary>
        /// Put update feedback status
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        void Put(EditFeedback request);
    }
}
