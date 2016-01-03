using System;
using System.Linq;
using System.Data.Entity;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;

namespace Anatoli.DataAccess.Repositories
{
    public class BaseValueRepository : AnatoliRepository<BaseValue>, IBaseValueRepository
    {
        #region Ctors
        public BaseValueRepository() : this(new AnatoliDbContext()) { }
        public BaseValueRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion
    }
}
