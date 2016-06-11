﻿using Anatoli.DataAccess;
using Anatoli.DataAccess.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.Business.Domain.Application
{
    public class ResourceDomain : RawBusinessDomain<ApplicationResourceRepository>
    {
        public ResourceDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey) :
            this(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, new AnatoliDbContext())
        {
        }

        public ResourceDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey, AnatoliDbContext context)
            : base(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey)
        {
            MainRepository = new ApplicationResourceRepository(context);
        }

    }
}
