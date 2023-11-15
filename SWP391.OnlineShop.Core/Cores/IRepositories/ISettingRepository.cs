using SWP391.OnlineShop.Core.Cores.Infrastructures;
using SWP391.OnlineShop.Core.Models.Entities;

namespace SWP391.OnlineShop.Core.Cores.IRepositories
{
    public interface ISettingRepository : IGenericRepository<Setting, int>
    {
        Task<int> GetSettingIdByType(string type);
        Task<List<Setting>> GetSettingByType(string type);
        Task<List<Setting>> GetSettingByStatus(string status);
        Task<List<Setting>> GetSettingWithPaging(int skip, int take);


        // User Settings
        List<string> GetRolesByUserId(int userId);
        string GetDefaultAddressByUserId(int userId);
    }
}
