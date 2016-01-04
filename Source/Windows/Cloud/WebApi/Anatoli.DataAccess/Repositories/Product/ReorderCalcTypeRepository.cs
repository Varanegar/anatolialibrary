using System;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;

namespace Anatoli.DataAccess.Repositories
{
    public class ReorderCalcTypeRepository : AnatoliRepository<ReorderCalcType>, IReorderCalcTypeRepository
    {
        #region Ctors
        public ReorderCalcTypeRepository() : this(new AnatoliDbContext()) { }
        public ReorderCalcTypeRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion
    }
}