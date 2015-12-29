using System;
using System.Linq;
using System.Data.Entity;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
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
        #endregion
    }
}
