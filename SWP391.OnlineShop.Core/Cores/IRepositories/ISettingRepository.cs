using SWP391.OnlineShop.Core.Cores.Infrastructures;
using SWP391.OnlineShop.Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.OnlineShop.Core.Cores.IRepositories
{
    public interface ISettingRepository: IGenericRepository<Setting, int>
    {
        Task<int> GetSettingIdByType(string type);
        Task<List<Setting>> GetSettingByOrderId(int orderId);


        Task<List<Setting>> GetSettingByType(string type);



        Task<List<Setting>> GetSettingByStatus(string status);



        Task<List<Setting>> GetSettingWithPaging(int skip, int take);
    }
}
