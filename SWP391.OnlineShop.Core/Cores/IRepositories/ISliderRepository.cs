using SWP391.OnlineShop.Core.Cores.Infrastructures;
using SWP391.OnlineShop.Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.OnlineShop.Core.Cores.IRepositories
{
    public interface ISliderRepository: IGenericRepository<Slider, int>
    {
        Task<int> GetSliderIdByTitle(string title);
        Task<List<Slider>> GetSliderByBlackLink(string blackLink);


        Task<List<Slider>> GetSliderByTitle(string title);



        Task<List<Slider>> GetSliderByStatus(string status);



        Task<List<Slider>> GetSliderWithPaging(int skip, int take);
    }
}
