using System;
using System.Linq;
using System.Data.Entity;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;

namespace Anatoli.DataAccess.Repositories
{
    public class BaseTypeRepository : AnatoliRepository<BaseType>, IBaseTypeRepository
    {
        #region Ctors
        public BaseTypeRepository() : this(new AnatoliDbContext()) { }
        public BaseTypeRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion
    }
}
