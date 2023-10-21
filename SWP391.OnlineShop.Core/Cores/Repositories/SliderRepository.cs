using Microsoft.EntityFrameworkCore;
using SWP391.OnlineShop.Core.Contexts;
using SWP391.OnlineShop.Core.Cores.Infrastructures;
using SWP391.OnlineShop.Core.Cores.IRepositories;
using SWP391.OnlineShop.Core.Models.Entities;
using SWP391.OnlineShop.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.OnlineShop.Core.Cores.Repositories
{
    public class SliderRepository : GenericRepository<Slider, int>, ISliderRepository
    {
        public SliderRepository(OnlineShopContext context) : base(context)
        {


        }

        public Task<List<Slider>> GetSliderByBlackLink(string blackLink)
        {
            var result = new List<Slider>();
            if (Context.Slider == null) return Task.FromResult(result);



            var sliders = Context.Slider.Where(x => x.BlackLink.ToString() == blackLink)
                .ToList();



            result = sliders.ToList();



            return Task.FromResult(result);
        }

        public Task<List<Slider>> GetSliderByStatus(string status)
        {
            var result = new List<Slider>();
            if (Context.Slider == null) return Task.FromResult(result);



            var sliders = Context.Slider.Where(x => x.Status.ToString() == status)
                .ToList();



            result = sliders.ToList();



            return Task.FromResult(result);
        }

        public Task<List<Slider>> GetSliderByTitle(string title)
        {
            var result = new List<Slider>();
            if (Context.Slider == null) return Task.FromResult(result);



            var sliders = Context.Slider.Where(x => x.Title.ToString() == title)
                .ToList();



            result = sliders.ToList();



            return Task.FromResult(result);
        }

        public async Task<int> GetSliderIdByTitle(string title)
        {
            var result = 0;



            if (Context.Slider != null)
            {
                result = await Context.Slider
                    .Where(p => p.Title != null &&
                                p.Title.ToLower().Equals(title.ToLower()))
                    .Select(p => p.Id)
                    .FirstOrDefaultAsync();
            }



            return result;
        }

        public Task<List<Slider>> GetSliderWithPaging(int skip, int take)
        {
            var result = new List<Slider>();
            if (Context.Slider == null) return Task.FromResult(result);



            var sliders = Context.Slider.Skip(skip).Take(take)
                .ToList();



            result = sliders.ToList();



            return Task.FromResult(result);
        }
        public override void Add(Slider? entity)
        {
            if (entity != null && Context.Slider != null)
            {
                Context.Slider.Add(entity);
            }
        }



        public override async Task AddAsync(Slider? entity)
        {
            if (entity != null && Context.Slider != null)
            {
                await Context.Slider.AddAsync(entity);
            }
        }
    }
}
