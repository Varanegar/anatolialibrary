using System;
using System.Linq;
using System.Collections.Generic;
using Anatoli.DataAccess.Models.Identity;

namespace Anatoli.DataAccess.Interfaces
{
    public interface IPrincipalRepository : IRepository<ApplicationOwner>
    {
    }
}
