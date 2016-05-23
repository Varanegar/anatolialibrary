using Anatoli.DataAccess.Models;
using Anatoli.DataAccess.Interfaces;

namespace Anatoli.DataAccess.Repositories
{
    public class CityRegionRepository : AnatoliRepository<CityRegion>, ICityRegionRepository
    {
        #region Ctors
        public CityRegionRepository() : this(new AnatoliDbContext()) { }
        public CityRegionRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        public CityRegionRepository(AnatoliDbContext context,OwnerInfo ownerInfo)
            : base(context, ownerInfo)
        {
        }
        #endregion
    }
}
