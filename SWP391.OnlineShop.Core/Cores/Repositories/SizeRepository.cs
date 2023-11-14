using SWP391.OnlineShop.Core.Contexts;
using SWP391.OnlineShop.Core.Cores.Infrastructures;
using SWP391.OnlineShop.Core.Cores.IRepositories;
using SWP391.OnlineShop.Core.Models.Entities;

namespace SWP391.OnlineShop.Core.Cores.Repositories
{
    public class SizeRepository : GenericRepository<Size, int>, ISizeRepository
    {
        public SizeRepository(OnlineShopContext context) : base(context)
        {
        }

        public Size GetSizeByName(string sizeType)
        {
            var result = new Size();
            if (Context.Sizes == null) return result;

            var size = Context.Sizes
                .Where(x => x.SizeType == sizeType).FirstOrDefault();

            result = size;

            return result;
        }
    }
}
