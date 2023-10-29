﻿using SWP391.OnlineShop.ServiceModel.Results;
using SWP391.OnlineShop.ServiceModel.ServiceModels;
using SWP391.OnlineShop.ServiceModel.ViewModels.Products;
using SWP391.OnlineShop.ServiceModel.ViewModels.Tags;

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
        /// Post Detail Standard & Criteria of Appendices
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Post Detail Standard & Criteria of Appendices</returns>
        List<PostViewModel> Get(GetPagingPost request);

        /// <summary>
        /// Post Appendix By Id
        /// </summary>
        /// <param name="request"></param>
        /// <returns>appendix of selected id</returns>
        PostViewModel Get(GetPostById request);

        /// <summary>
        /// Post Appendix By Id
        /// </summary>
        /// <param name="request"></param>
        /// <returns>appendix of selected id</returns>
        List<PostViewModel> Get(GetPostByCategory request);

        /// <summary>
        /// Post Appendix By Id
        /// </summary>
        /// <param name="request"></param>
        /// <returns>appendix of selected id</returns>
        List<PostViewModel> Get(GetPostByAuthor request);

        /// <summary>
        /// Post Appendix By Id
        /// </summary>
        /// <param name="request"></param>
        /// <returns>appendix of selected id</returns>
        List<PostViewModel> Get(GetPostByStatus request);

        /// <summary>
        /// Post Appendix By Id
        /// </summary>
        /// <param name="request"></param>
        /// <returns>appendix of selected id</returns>
        List<PostViewModel> Get(GetPostByTitle request);

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
        BaseResultModel Put(PutUpdatePost request);

        /// <summary>
        /// Get Tag by Post
        /// </summary>
        /// <param name="request"></param>
        /// <returns>List of tag</returns>
		List<TagViewModel> Get(GetTagByPost request);

        /// <summary>
        /// Get All Tag
        /// </summary>
        /// <param name="request"></param>
        /// <returns>List of tag</returns>
		List<TagViewModel> Get(GetAllTag request);
	}
}
