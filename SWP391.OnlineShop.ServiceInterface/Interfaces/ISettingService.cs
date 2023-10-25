using SWP391.OnlineShop.ServiceModel.ViewModels.Settings;
using static SWP391.OnlineShop.ServiceModel.ServiceModels.SettingModels;

namespace SWP391.OnlineShop.ServiceInterface.Interfaces
{
    public interface ISettingService
    {

        /// <summary>
        /// Post Detail Standard & Criteria of Appendices
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Post Detail Standard & Criteria of Appendices</returns>
        List<SettingViewModel> Get(GetAllSetting request);

        /// <summary>
        /// Post Appendix By Id
        /// </summary>
        /// <param name="request"></param>
        /// <returns>appendix of selected id</returns>
        SettingViewModel Get(GetSettingById request);

        /// <summary>
        /// Add new Appendix
        /// </summary>
        /// <param name="request"></param>
        /// <returns>appendix of selected id</returns>
        Task<SettingViewModel> Post(PostAddSetting request);

        /// <summary>
        /// Add new Appendix
        /// </summary>
        /// <param name="request"></param>
        /// <returns>appendix of selected id</returns>
        Task<SettingViewModel> Delete(DeleteSetting request);

        /// <summary>
        /// Add new Appendix
        /// </summary>
        /// <param name="request"></param>
        /// <returns>appendix of selected id</returns>
        Task<SettingViewModel> Put(PutUpdateSetting request);
    }
}
