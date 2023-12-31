﻿using SWP391.OnlineShop.ServiceModel.Results;
using SWP391.OnlineShop.ServiceModel.ViewModels.Settings;
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
        Task<BaseResultModel> Post(PostAddSlider request);

        /// <summary>
        /// Add new Appendix
        /// </summary>
        /// <param name="request"></param>
        /// <returns>appendix of selected id</returns>
        Task<BaseResultModel> Delete(DeleteSlider request);

        /// <summary>
        /// Add new Appendix
        /// </summary>
        /// <param name="request"></param>
        /// <returns>appendix of selected id</returns>
        Task<BaseResultModel> Put(PutUpdateSlider request);
    }
}
