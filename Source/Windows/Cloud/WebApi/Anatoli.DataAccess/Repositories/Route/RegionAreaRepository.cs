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