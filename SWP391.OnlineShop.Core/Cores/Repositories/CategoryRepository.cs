using Microsoft.EntityFrameworkCore;
using SWP391.OnlineShop.Core.Contexts;
using SWP391.OnlineShop.Core.Cores.Infrastructures;
using SWP391.OnlineShop.Core.Cores.IRepositories;
using SWP391.OnlineShop.Core.Models.Entities;

namespace SWP391.OnlineShop.Core.Cores.Repositories;

public class CategoryRepository : GenericRepository<Category, int>, ICategoryRepository
{
    public CategoryRepository(OnlineShopContext context) : base(context)
    {
    }

    public override List<Category> GetAll()
    {
        var result = new List<Category>();
        if (Context.Categories == null) return result;
        var products = Context
                        .Categories
                        .Include(x => x.Products)
                        .ToList();

        result = products;

        return result;
    }
}