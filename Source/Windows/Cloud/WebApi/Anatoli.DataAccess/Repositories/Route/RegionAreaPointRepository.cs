using System;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;
using Anatoli.DataAccess.Models.Route;

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