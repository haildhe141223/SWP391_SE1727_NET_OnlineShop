using SWP391.OnlineShop.Core.Cores.Infrastructures;
using SWP391.OnlineShop.Core.Models.Entities;

 

namespace SWP391.OnlineShop.Core.Cores.IRepositories;

 

public interface IProductRepository : IGenericRepository<Product, int>
{
    Task<int> GetProductIdByProductName(string productName);
    Task<string> GetProductNameByProductId(int productId);
    Task<List<Product>> GetProductNameByCategoryId(int categoryId);

 

    Task<List<Product>> GetProductsByName(string productName);

 

    Task<List<Product>> GetProductsByStatus(string status);

 

    Task<List<Product>> GetProductsWithPaging(int skip, int take);