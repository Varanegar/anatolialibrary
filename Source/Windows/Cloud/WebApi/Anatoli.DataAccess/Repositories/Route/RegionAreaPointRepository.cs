using Anatoli.DataAccess.Interfaces;
using Anatoli.DataAccess.Models.Route;
using Anatoli.Common.DataAccess.Repositories;

namespace Anatoli.DataAccess.Repositories
{
    public class RegionAreaPointRepository : AnatoliRepository<RegionAreaPoint>, IRegionAreaPointRepository
    {
        #region Ctors
        public RegionAreaPointRepository() : this(new AnatoliDbContext()) { }
        public RegionAreaPointRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion
    }
}