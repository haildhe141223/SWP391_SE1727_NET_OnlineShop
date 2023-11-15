using Microsoft.EntityFrameworkCore;
using SWP391.OnlineShop.Core.Contexts;
using SWP391.OnlineShop.Core.Cores.Infrastructures;
using SWP391.OnlineShop.Core.Cores.IRepositories;
using SWP391.OnlineShop.Core.Models.Entities;

namespace SWP391.OnlineShop.Core.Cores.Repositories
{
    public class SettingRepository : GenericRepository<Setting, int>, ISettingRepository
    {
        public SettingRepository(OnlineShopContext context) : base(context)
        {
        }

        public Task<List<Setting>> GetSettingByStatus(string status)
        {
            var result = new List<Setting>();
            if (Context.Settings == null) return Task.FromResult(result);

            var settings = Context.Settings.Where(x => x.Status.ToString() == status)
                .ToList();

            result = settings.ToList();

            return Task.FromResult(result);
        }

        public Task<List<Setting>> GetSettingByType(string type)
        {
            var result = new List<Setting>();
            if (Context.Settings == null) return Task.FromResult(result);

            var settings = Context.Settings.Where(x => x.Type.ToString() == type)
                .ToList();

            result = settings.ToList();

            return Task.FromResult(result);
        }

        public async Task<int> GetSettingIdByType(string type)
        {
            var result = 0;

            if (Context.Settings != null)
            {
                result = await Context.Settings
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
            if (Context.Settings == null) return Task.FromResult(result);

            var settings = Context.Settings.Skip(skip).Take(take)
                .ToList();

            result = settings.ToList();

            return Task.FromResult(result);
        }

        public override void Add(Setting entity)
        {
            if (entity != null && Context.Settings != null)
            {
                Context.Settings.Add(entity);
            }
        }

        public override async Task AddAsync(Setting entity)
        {
            if (entity != null && Context.Settings != null)
            {
                await Context.Settings.AddAsync(entity);
            }
        }

        #region User Settings
        public List<string> GetRolesByUserId(int userId)
        {
            var roles = new List<string>();

            var user = Context.Users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                var userRoles = Context.UserRoles
                    .Where(ur => ur.UserId == user.Id)
                    .Distinct()
                    .ToList();
                for (var i = 0; i < userRoles.Count(); i++)
                {
                    var data = userRoles[i];
                    var role = Context.Roles.FirstOrDefault(r => r.Id == data.RoleId);
                    if (role != null)
                        roles.Add(role.Name);
                }
            }

            return roles;
        }

        public string GetDefaultAddressByUserId(int userId)
        {
            var user = Context.Users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                var address = Context.Addresses.FirstOrDefault(r => r.UserId == userId && r.IsDefault);
                if (address != null) return address.FullAddress;
            }
            return string.Empty;
        }

        #endregion
    }
}
