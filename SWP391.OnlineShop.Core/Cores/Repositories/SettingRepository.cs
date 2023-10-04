using Microsoft.EntityFrameworkCore;
using SWP391.OnlineShop.Core.Contexts;
using SWP391.OnlineShop.Core.Cores.Infrastructures;
using SWP391.OnlineShop.Core.Cores.IRepositories;
using SWP391.OnlineShop.Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.OnlineShop.Core.Cores.Repositories
{
    public class SettingRepository: GenericRepository<Setting, int>, ISettingRepository
    {
        public SettingRepository(OnlineShopContext context) : base(context)
        {



        }


        public async Task<string> GetSettingNameByProductId(int productId)
        {
            var result = string.Empty;
            if (Context.Products != null)
            {
                result = await Context.Products
                     .Where(p => p.Id == productId)
                     .Select(p => p.ProductName)
                     .FirstOrDefaultAsync();
            }



            return result ?? string.Empty;
        }



        public Task<List<Product>> GetProductNameByCategoryId(int categoryId)
        {
            var result = new List<Product>();
            if (Context.Products == null) return Task.FromResult(result);



            var products = Context.Products.Where(x => x.CategoryId == categoryId)
                .ToList();



            result = products.ToList();



            return Task.FromResult(result);
        }



        public Task<List<Product>> GetProductsByName(string productName)
        {
            var result = new List<Product>();
            if (Context.Products == null) return Task.FromResult(result);



            var products = Context.Products.Where(x => x.ProductName.ToLower().Contains(productName.ToLower()))
                .ToList();



            result = products.ToList();



            return Task.FromResult(result);
        }



        public Task<List<Product>> GetProductsByStatus(string status)
        {
            var result = new List<Product>();
            if (Context.Products == null) return Task.FromResult(result);



            var products = Context.Products.Where(x => x.Status.ToString() == status)
                .ToList();



            result = products.ToList();



            return Task.FromResult(result);
        }



        public Task<List<Product>> GetProductsWithPaging(int skip, int take)
        {
            var result = new List<Product>();
            if (Context.Products == null) return Task.FromResult(result);



            var products = Context.Products.Skip(skip).Take(take)
                .ToList();



            result = products.ToList();



            return Task.FromResult(result);
        }



        public override void Add(Setting? entity)
        {
            if (entity != null && Context.Setting != null)
            {
                Context.Products.Add(entity);
            }
        }



        public override async Task AddAsync(Setting? entity)
        {
            if (entity != null && Context.Setting != null)
            {
                await Context.Products.AddAsync(entity);
            }
        }
    }
}
