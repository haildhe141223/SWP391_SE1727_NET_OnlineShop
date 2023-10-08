using Microsoft.EntityFrameworkCore;
using SWP391.OnlineShop.Core.Contexts;
using SWP391.OnlineShop.Core.Cores.Infrastructures;
using SWP391.OnlineShop.Core.Cores.IRepositories;
using SWP391.OnlineShop.Core.Models.Entities;

namespace SWP391.OnlineShop.Core.Cores.Repositories;

public class ProductRepository : GenericRepository<Product, int>, IProductRepository
{
    public ProductRepository(OnlineShopContext context) : base(context)
    {


    }


    public async Task<int> GetProductIdByProductName(string productName)
    {
        var result = 0;

        if (Context.Products != null)
        {
            result = await Context.Products
                .Where(p => p.ProductName != null &&
                            p.ProductName.ToLower().Equals(productName.ToLower()))
                .Select(p => p.Id)
                .FirstOrDefaultAsync();
        }

        return result;
    }

    public async Task<string> GetProductNameByProductId(int productId)
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

        var products = Context.Products
            .Where(x => x.CategoryId == categoryId)
            .ToList();

        result = products.ToList();

        return Task.FromResult(result);
    }



    public Task<List<Product>> GetProductsByName(string productName)
    {
        var result = new List<Product>();
        if (Context.Products == null) return Task.FromResult(result);

        var products = Context.Products
            .Where(x => x.ProductName.ToLower().Contains(productName.ToLower()))
            .ToList();

        result = products.ToList();

        return Task.FromResult(result);
    }


    public Task<List<Product>> GetProductsByStatus(string status)
    {
        var result = new List<Product>();
        if (Context.Products == null) return Task.FromResult(result);

        var products = Context.Products
            .Where(x => x.Status.ToString() == status)
            .ToList();

        result = products.ToList();

        return Task.FromResult(result);
    }



    public Task<List<Product>> GetProductsWithPaging(int skip, int take)
    {
        var result = new List<Product>();
        if (Context.Products == null) return Task.FromResult(result);

        var products = Context.Products
            .Skip(skip)
            .Take(take)
            .ToList();

        result = products.ToList();

        return Task.FromResult(result);
    }



    public override void Add(Product entity)
    {
        if (entity != null && Context.Products != null)
        {
            Context.Products.Add(entity);
        }
    }



    public override async Task AddAsync(Product entity)
    {
        if (entity != null && Context.Products != null)
        {
            await Context.Products.AddAsync(entity);
        }
    }


    public Task<List<Product>> GetHotDealProduct()
    {
        var result = new List<Product>();
        if (Context.OrderDetails == null) return Task.FromResult(result);
        var products = Context
                        .OrderDetails
                        .GroupBy(x => x.Product.Id)
                        .OrderByDescending(g => g.Count())
                        .SelectMany(g => g.Select(p => p.Product))
                        .Take(5)
                        .ToList();

        result = products;

        return Task.FromResult(result);
    }


    public Task<List<Product>> GetDealProductOfWeek()
    {
        var result = new List<Product>();
        if (Context.OrderDetails == null) return Task.FromResult(result);
        var products = Context
                        .Products
                        .Include(x => x.OrderDetails
                        .GroupBy(x => x.ProductId)
                        .Select(n => new
                        {
                            productId = n.Key,
                            productCount = n.Count(),

                        })
                        .OrderByDescending(n => n.productCount)).Take(9)
                        .ToList();

        result = products;

        return Task.FromResult(result);
    }

    public Task<List<Product>> GetProductFeedbackById(int productId)
    {
        var result = new List<Product>();
        if (Context.Products == null) return Task.FromResult(result);

        var products = Context.Products
            .Where(x => x.Id == productId).Include(x => x.FeedBacks)
            .ToList();

        result = products.ToList();

        return Task.FromResult(result);
    }
}