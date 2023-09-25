using SWP391.OnlineShop.ServiceModel.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SWP391.OnlineShop.ServiceModel.ServiceModels.ProductModels;

namespace SWP391.OnlineShop.ServiceInterface.Interfaces
{
    public interface IProductService
    {
        /// <summary>
        /// Post Detail Standard & Criteria of Appendices
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Post Detail Standard & Criteria of Appendices</returns>
        List<ProductViewModel> Get(GetAllProduct request);

        /// <summary>
        /// Post Appendix By Id
        /// </summary>
        /// <param name="request"></param>
        /// <returns>appendix of selected id</returns>
        ProductViewModel Get(GetProductById request);

        /// <summary>
        /// Add new Appendix
        /// </summary>
        /// <param name="request"></param>
        /// <returns>appendix of selected id</returns>
        Task<ProductViewModel> Post(PostAddProduct request);

        /// <summary>
        /// Add new Appendix
        /// </summary>
        /// <param name="request"></param>
        /// <returns>appendix of selected id</returns>
        Task<ProductViewModel> Delete(DeleteProduct request);

        /// <summary>
        /// Add new Appendix
        /// </summary>
        /// <param name="request"></param>
        /// <returns>appendix of selected id</returns>
        Task<ProductViewModel> Put(PutUpdateProduct request);
    }
}

