using System;
using System.Linq;
using System.Data.Entity;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;
using Anatoli.Common.DataAccess.Repositories;

namespace Anatoli.DataAccess.Repositories
{
    public class LoyaltyProgramRepository : AnatoliRepository<LoyaltyProgram>, ILoyaltyProgramRepository
    {
        #region Ctors
        public LoyaltyProgramRepository() : this(new AnatoliDbContext()) { }
        public LoyaltyProgramRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion
    }
}
