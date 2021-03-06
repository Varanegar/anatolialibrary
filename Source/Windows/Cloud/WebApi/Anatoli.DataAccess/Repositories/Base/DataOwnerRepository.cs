﻿using Anatoli.DataAccess.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Anatoli.Common.DataAccess.Repositories;
using Anatoli.Common.DataAccess.Models;

namespace Anatoli.DataAccess.Repositories.Base
{
    public class DataOwnerRepository : BaseAnatoliRepository<DataOwner>
    {
        public DataOwnerRepository(AnatoliDbContext dbContext) : base(dbContext)
        {
        }

        public DataOwnerRepository(AnatoliDbContext dbContext, OwnerInfo ownerInfo) : base(dbContext, ownerInfo)
        {
        }

        public IEnumerable<DataOwner> GetDataOwnerWithDetails()
        {
            return DbContext.Set<DataOwner>().Include(dataOwner => dataOwner.ApplicationOwner).Include(dataOwner => dataOwner.AnatoliContact).ToList();
        }
    }
}
