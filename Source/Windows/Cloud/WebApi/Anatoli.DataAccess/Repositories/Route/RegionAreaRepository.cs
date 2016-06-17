using Anatoli.DataAccess.Interfaces;
using Anatoli.DataAccess.Models.Route;
using Anatoli.Common.DataAccess.Repositories;

namespace Anatoli.DataAccess.Repositories
{
    public class RegionAreaRepository : AnatoliRepository<RegionArea>, IRegionAreaRepository
    {
        #region Ctors
        public RegionAreaRepository() : this(new AnatoliDbContext()) { }
        public RegionAreaRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion
    }
}