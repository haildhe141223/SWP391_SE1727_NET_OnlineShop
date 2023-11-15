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


    public List<Product> GetProductByCategoryId(int categoryId)
    {
        var result = new List<Product>();
        if (Context.Products == null) return result;

        var products = Context.Products
            .Where(x => x.CategoryId == categoryId && x.Status == Models.Enums.Status.Active)
            .ToList();

        result = products.ToList();

        return result;
    }



    public Task<List<Product>> GetProductsByName(string productName)
    {
        var result = new List<Product>();
        if (Context.Products == null) return Task.FromResult(result);

        var products = Context.Products
            .Where(x => x.Status == Models.Enums.Status.Active && x.ProductName.ToLower().Contains(productName.ToLower()))
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
            .Where(x => x.Status == Models.Enums.Status.Active)
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
                        .Where(x => x.Status == Models.Enums.Status.Active)
                        .Take(5)
                        .ToList();

        result = products;

        return Task.FromResult(result);
    }


    public List<Product> GetDealProductOfWeek()
    {
        var result = new List<Product>();
        if (Context.Products == null) return result;
        var products = Context
                        .Products
                        .Where(x => x.Status == Models.Enums.Status.Active)
                        .OrderByDescending(x => x.OrderDetails.Count)
                        .Take(6)
                        .ToList();

        result = products;

        return result;
    }

    public Product GetProductFeedbackById(int productId)
    {
        var result = new Product();
        if (Context.Products == null) return result;

        var products = Context.Products
            .Where(x => x.Id == productId).Include(x => x.Category).Include(x => x.FeedBacks).ThenInclude(f => f.User)
            .Include(x => x.ProductSizes).ThenInclude(s => s.Size).Include(x => x.ProductTags).ThenInclude(p => p.Tag).FirstOrDefault();

        result = products;

        return result;
    }

	public List<Product> GetProductByTagId(int tagId)
	{
		var result = new List<Product>();
		if (Context.Products == null) return result;

		var products = Context.Products.Include(x => x.ProductTags)
			.Where(x => x.ProductTags.Any(x => x.TagId == tagId) && x.Status == Models.Enums.Status.Active)
			.ToList();

		result = products.ToList();

		return result;
	}
}