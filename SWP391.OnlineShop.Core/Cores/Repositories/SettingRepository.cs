﻿using Microsoft.EntityFrameworkCore;
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

        public Task<List<Setting>> GetSettingByOrderId(int orderId)
        {
            var result = new List<Setting>();
            if (Context.Setting == null) return Task.FromResult(result);



            var settings = Context.Setting.Where(x => x.OrderId == orderId)
                .ToList();



            result = settings.ToList();



            return Task.FromResult(result);
        }

        public Task<List<Setting>> GetSettingByStatus(string status)
        {
            var result = new List<Setting>();
            if (Context.Setting == null) return Task.FromResult(result);



            var settings = Context.Setting.Where(x => x.Status.ToString() == status)
                .ToList();



            result = settings.ToList();



            return Task.FromResult(result);
        }

        public Task<List<Setting>> GetSettingByType(string type)
        {
            var result = new List<Setting>();
            if (Context.Setting == null) return Task.FromResult(result);



            var settings = Context.Setting.Where(x => x.Type.ToString() == type)
                .ToList();



            result = settings.ToList();



            return Task.FromResult(result);
        }

        public async Task<int> GetSettingIdByType(string type)
        {
            var result = 0;



            if (Context.Setting != null)
            {
                result = await Context.Setting
                    .Where(p => p.Type != null &&
                                p.Type.ToLower().Equals(type.ToLower()))
                    .Select(p => p.Id)
                    .FirstOrDefaultAsync();
            }



            return result;
        }

        public Task<List<Setting>> GetSettingWithPaging(int skip, int take)
        {
            var result = new List<Setting>();
            if (Context.Setting == null) return Task.FromResult(result);



            var settings = Context.Setting.Skip(skip).Take(take)
                .ToList();



            result = settings.ToList();



            return Task.FromResult(result);
        }

        public override void Add(Setting? entity)
        {
            if (entity != null && Context.Setting != null)
            {
                Context.Setting.Add(entity);
            }
        }



        public override async Task AddAsync(Setting? entity)
        {
            if (entity != null && Context.Setting != null)
            {
                await Context.Setting.AddAsync(entity);
            }
        }
    }
}
