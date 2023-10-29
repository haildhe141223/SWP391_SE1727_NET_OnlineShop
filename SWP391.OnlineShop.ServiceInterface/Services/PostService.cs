using AutoMapper;
using SWP391.OnlineShop.Common.Enums;
using SWP391.OnlineShop.Core.Cores.UnitOfWork;
using SWP391.OnlineShop.Core.Models.Entities;
using SWP391.OnlineShop.ServiceInterface.BaseServices;
using SWP391.OnlineShop.ServiceInterface.Interfaces;
using SWP391.OnlineShop.ServiceInterface.Loggers;
using SWP391.OnlineShop.ServiceModel.Results;
using SWP391.OnlineShop.ServiceModel.ServiceModels;
using SWP391.OnlineShop.ServiceModel.ViewModels.Products;
using SWP391.OnlineShop.ServiceModel.ViewModels.Tags;

namespace SWP391.OnlineShop.ServiceInterface.Services
{
    //TODO: PhuongNL logger should have key to double check in log. Check AccountService for example
    public class PostService : BaseService, IPostService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerService _logger;

        public PostService(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ILoggerService logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
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
                    return result;
                }
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
                var post = _unitOfWork.Posts.GetAllPost();
                result = _mapper.Map<List<PostViewModel>>(post);
                return result;
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
                return result;
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
                    Thumbnail = request.Thumbnail,
                    Author = request.Author,
                    Brief = request.Brief,
                    Description = request.Description,
                    Featured = request.Featured,
                    CategoryId = request.CategoryId,
                    Status = Core.Models.Enums.Status.Active,
                    CreatedDateTime = DateTime.Now
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
            try
            {
                var post = _unitOfWork.Posts.GetById(request.Id);

                if (post != null)
                {
                    post.Title = request.Title;
                    post.Thumbnail = request.Thumbnail;
                    post.Author = request.Author;
                    post.Brief = request.Brief;
                    post.Description = request.Description;
                    post.Featured = request.Featured;
                    post.CategoryId = request.CategoryId;
                    post.Status = request.Status;

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
		public List<TagViewModel> Get(GetTagByPost request)
		{
			var result = new List<TagViewModel>();

			try
			{
				var query = from t in _unitOfWork.Context.Tags
							join pt in _unitOfWork.Context.PostTags
							on t.Id equals pt.TagId
							join p in _unitOfWork.Context.Posts
							on pt.PostId equals p.Id
							where p.Id == request.PostId
							select t;
				var listTag = query.ToList();
				if (listTag.Any())
				{
					foreach (var tag in listTag)
					{
						var tagVm = _mapper.Map<TagViewModel>(tag);
						result.Add(tagVm);
					}
				}

			}
			catch (Exception ex)
			{
				_logger.LogError("GetTagByPost error" + ex.Message);
			}
			return result;
		}

		public List<TagViewModel> Get(GetAllTag request)
		{
			var result = new List<TagViewModel>();

			try
			{
				var query = _unitOfWork.Context.Tags;
				var listTag = query.ToList();
				if (listTag.Any())
				{
					foreach (var tag in listTag)
					{
						var tagVm = _mapper.Map<TagViewModel>(tag);
						result.Add(tagVm);
					}
				}

			}
			catch (Exception ex)
			{
				_logger.LogError("GetTagByPost error" + ex.Message);
			}
			return result;
		}
	}
}
