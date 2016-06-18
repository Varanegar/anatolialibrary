using Anatoli.DataAccess;
using Anatoli.DataAccess.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.Business.Domain.Application
{
    public class DataOwnerDomain : RawBusinessDomain<DataOwnerRepository>
    {
        public DataOwnerDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey) : base(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey)
        {
            MainRepository = new DataOwnerRepository(new AnatoliDbContext());
        }
    }
}
