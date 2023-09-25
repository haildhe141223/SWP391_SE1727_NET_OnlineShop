﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using SWP391.OnlineShop.Common.Enums;
using SWP391.OnlineShop.Core.Cores.UnitOfWork;
using SWP391.OnlineShop.Core.Models.Entities;
using SWP391.OnlineShop.Core.Models.Identities;
using SWP391.OnlineShop.ServiceInterface.BaseServices;
using SWP391.OnlineShop.ServiceInterface.Interfaces;
using SWP391.OnlineShop.ServiceInterface.Loggers;
using SWP391.OnlineShop.ServiceModel.ServiceModels;
using SWP391.OnlineShop.ServiceModel.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.OnlineShop.ServiceInterface.Services
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerService _logger;
        private readonly UserManager<User> _userManager;

        public ProductService(
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
        public async Task<ProductViewModel> Delete(ProductModels.DeleteProduct request)
        {
            var result = new ProductViewModel();
            try
            {
                _unitOfWork.Products.Delete(request.ProductId);
                var rows = await _unitOfWork.CompleteAsync();
                if (rows > 0)
                {
                    var product = await _unitOfWork.Products.GetByIdAsync(request.ProductId);
                    result = _mapper.Map<ProductViewModel>(product);
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

        public  List<ProductViewModel> Get(ProductModels.GetAllProduct request)
        {
            var result = new List<ProductViewModel>();
            try
            {
                var product = _unitOfWork.Products.GetAll().ToList();
                result = _mapper.Map<List<ProductViewModel>>(product);
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

        public ProductViewModel Get(ProductModels.GetProductById request)
        {
            var result = new ProductViewModel();
            try
            {
                var appendix = _unitOfWork.Products.GetById(request.ProductId);
                if (appendix != null)
                {
                    result = _mapper.Map<ProductViewModel>(appendix);
                    //result.StatusCode = StatusCode.Success;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return result;
        }

        public async Task<ProductViewModel> Post(ProductModels.PostAddProduct request)
        {
            var result = new ProductViewModel();
            try
            {
                var product = new Product()
                {
                     ProductName = request.ProductName,
                     Thumbnail = result.Thumbnail,
                     Amount = request.Amount,
                     Price = request.Price,
                     SalePrice = request.SalePrice,
                     CategoryId = request.CategoryId,
                    Status = Core.Models.Enums.Status.Active
                };
                await _unitOfWork.Products.AddAsync(product);
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return result;
        }

        public async Task<ProductViewModel> Put(ProductModels.PutUpdateProduct request)
        {
            var result = new ProductViewModel();
            try
            {
                var product = _unitOfWork.Products.GetById(request.Id);
                if (product == null)
                {
                    //result.StatusCode = StatusCode.InternalServerError;
                    //return result;
                }
                product.ProductName = request.ProductName;

                _unitOfWork.Products.Update(product);
                var rows = await _unitOfWork.CompleteAsync();
                //result.StatusCode = rows > 0 ? StatusCode.Success : StatusCode.InternalServerError;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return result;
        }
    }
}