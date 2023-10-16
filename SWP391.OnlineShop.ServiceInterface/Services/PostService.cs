using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SWP391.OnlineShop.Common.Enums;
using SWP391.OnlineShop.Core.Cores.UnitOfWork;
using SWP391.OnlineShop.Core.Models.Entities;
using SWP391.OnlineShop.Core.Models.Identities;
using SWP391.OnlineShop.ServiceInterface.BaseServices;
using SWP391.OnlineShop.ServiceInterface.Interfaces;
using SWP391.OnlineShop.ServiceInterface.Loggers;
using SWP391.OnlineShop.ServiceModel.Results;
using SWP391.OnlineShop.ServiceModel.ServiceModels;
using SWP391.OnlineShop.ServiceModel.ViewModels.Products;

namespace SWP391.OnlineShop.ServiceInterface.Services
{
    public class PostService : BaseService, IPostService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerService _logger;
        private readonly UserManager<User> _userManager;

        public PostService(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ILoggerService logger,
            UserManager<User> userManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<PostViewModel> Delete(DeletePost request)
        {
            var result = new PostViewModel();
            try
            {
                _unitOfWork.Posts.Delete(request.PostId);
                var rows = await _unitOfWork.CompleteAsync();
                if (rows > 0)
                {
                    var post = await _unitOfWork.Posts.GetByIdAsync(request.PostId);
                    result = _mapper.Map<PostViewModel>(post);
                    //result.StatusCode = StatusCode.Success;
                    return result;
                }
                //result.StatusCode = StatusCode.InternalServerError;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return result;
        }

        public List<PostViewModel> Get(GetAllPost request)
        {
            var result = new List<PostViewModel>();
            try
            {
                var post = _unitOfWork.Posts.GetAll().ToList();
                result = _mapper.Map<List<PostViewModel>>(post);
                //result.StatusCode = StatusCode.Success;
                return result;
                //result.StatusCode = StatusCode.InternalServerError;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return result;
        }

        public PostViewModel Get(GetPostById request)
        {
            var result = new PostViewModel();
            try
            {
                var post = _unitOfWork.Posts.GetById(request.PostId);
                if (post != null)
                {
                    result = _mapper.Map<PostViewModel>(post);
                    //result.StatusCode = StatusCode.Success;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return result;
        }

        public List<PostViewModel> Get(GetPagingPost request)
        {
            var result = new List<PostViewModel>();
            try
            {
                var post = _unitOfWork.Posts.GetAll()
                            .Skip(request.Skip)
                            .Take(request.Take)
                            .ToList();
                result = _mapper.Map<List<PostViewModel>>(post);
                //result.StatusCode = StatusCode.Success;
                return result;
                //result.StatusCode = StatusCode.InternalServerError;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return result;
        }

        public List<PostViewModel> Get(GetPostByCategory request)
        {
            var result = new List<PostViewModel>();
            try
            {
                var post = _unitOfWork.Posts
                    .GetPostsByCategoryId(request.CategoryId);
                if (post != null)
                {
                    result = _mapper.Map<List<PostViewModel>>(post);
                    //result.StatusCode = StatusCode.Success;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return result;
        }

        public List<PostViewModel> Get(GetPostByAuthor request)
        {
            var result = new List<PostViewModel>();
            try
            {
                var post = _unitOfWork.Posts
                    .GetPostsByAuthor(request.Author);
                if (post != null)
                {
                    result = _mapper.Map<List<PostViewModel>>(post);
                    //result.StatusCode = StatusCode.Success;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return result;
        }

        public List<PostViewModel> Get(GetPostByStatus request)
        {
            var result = new List<PostViewModel>();
            try
            {
                var post = _unitOfWork.Posts
                    .GetPostByStatus(request.Status);
                if (post != null)
                {
                    result = _mapper.Map<List<PostViewModel>>(post);
                    //result.StatusCode = StatusCode.Success;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return result;
        }

        public List<PostViewModel> Get(GetPostByTitle request)
        {
            var result = new List<PostViewModel>();
            try
            {
                var post = _unitOfWork.Posts
                    .GetPostByName(request.Title);
                if (post != null)
                {
                    result = _mapper.Map<List<PostViewModel>>(post);
                    //result.StatusCode = StatusCode.Success;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return result;
        }

        public async Task<PostViewModel> Post(PostAddPost request)
        {
            var result = new PostViewModel();
            try
            {
                var post = new Post()
                {
                    Title = request.Title,
                    Author = request.Author,
                    Brief = request.Brief,
                    Description = request.Description,
                    Featured = request.Featured,
                    CategoryId = request.CategoryId,
                    Status = Core.Models.Enums.Status.Active
                };
                await _unitOfWork.Posts.AddAsync(post);
                await _unitOfWork.CompleteAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return result;
        }

        public BaseResultModel Put(PutUpdatePost request)
        {
            var result = new PostViewModel();
            try
            {
                var post = _unitOfWork.Posts.GetById(request.Id);

                if (post != null)
                {
                    post.Title = request.Title;
                    post.Author = request.Author;
                    post.Brief = request.Brief;
                    post.Description = request.Description;
                    post.Featured = request.Featured;
                    post.CategoryId = request.CategoryId;

                    _unitOfWork.Posts.Update(post);
                    _unitOfWork.Complete();
                }

                return new BaseResultModel
                {
                    StatusCode = StatusCode.Success
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new BaseResultModel
                {
                    StatusCode = StatusCode.InternalServerError,
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}
