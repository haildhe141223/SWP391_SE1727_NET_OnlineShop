using SWP391.OnlineShop.ServiceModel.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SWP391.OnlineShop.ServiceModel.ServiceModels.PostModels;
using static SWP391.OnlineShop.ServiceModel.ServiceModels.ProductModels;

namespace SWP391.OnlineShop.ServiceInterface.Interfaces
{
    public interface IPostService
    {
        /// <summary>
        /// Post Detail Standard & Criteria of Appendices
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Post Detail Standard & Criteria of Appendices</returns>
        List<PostViewModel> Get(GetAllPost request);

        /// <summary>
        /// Post Appendix By Id
        /// </summary>
        /// <param name="request"></param>
        /// <returns>appendix of selected id</returns>
        PostViewModel Get(GetPostById request);

        /// <summary>
        /// Add new Appendix
        /// </summary>
        /// <param name="request"></param>
        /// <returns>appendix of selected id</returns>
        Task<PostViewModel> Post(PostAddPost request);

        /// <summary>
        /// Add new Appendix
        /// </summary>
        /// <param name="request"></param>
        /// <returns>appendix of selected id</returns>
        Task<PostViewModel> Delete(DeletePost request);

        /// <summary>
        /// Add new Appendix
        /// </summary>
        /// <param name="request"></param>
        /// <returns>appendix of selected id</returns>
        Task<PostViewModel> Put(PutUpdatePost request);
    }
}
