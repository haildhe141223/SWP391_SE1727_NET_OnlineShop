using SWP391.OnlineShop.ServiceModel.ViewModels.Customer;
using SWP391.OnlineShop.ServiceModel.ViewModels.Marketing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SWP391.OnlineShop.ServiceModel.ServiceModels.SettingModels;
using static SWP391.OnlineShop.ServiceModel.ServiceModels.SliderModel;

namespace SWP391.OnlineShop.ServiceInterface.Interfaces
{
    public interface ISliderService
    {

        /// <summary>
        /// Post Detail Standard & Criteria of Appendices
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Post Detail Standard & Criteria of Appendices</returns>
        List<SliderViewModel> Get(GetAllSlider request);

        /// <summary>
        /// Post Appendix By Id
        /// </summary>
        /// <param name="request"></param>
        /// <returns>appendix of selected id</returns>
        SliderViewModel Get(GetSliderById request);

        /// <summary>
        /// Add new Appendix
        /// </summary>
        /// <param name="request"></param>
        /// <returns>appendix of selected id</returns>
        Task<SliderViewModel> Post(PostAddSlider request);

        /// <summary>
        /// Add new Appendix
        /// </summary>
        /// <param name="request"></param>
        /// <returns>appendix of selected id</returns>
        Task<SliderViewModel> Delete(DeleteSlider request);

        /// <summary>
        /// Add new Appendix
        /// </summary>
        /// <param name="request"></param>
        /// <returns>appendix of selected id</returns>
        Task<SliderViewModel> Put(PutUpdateSlider request);
    }
}
