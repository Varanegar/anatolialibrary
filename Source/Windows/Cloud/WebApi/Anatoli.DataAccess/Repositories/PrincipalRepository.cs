using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;
using Anatoli.DataAccess.Models.Identity;

namespace Anatoli.DataAccess.Repositories
{
    public class PrincipalRepository : AnatoliRepository<Principal>, IPrincipalRepository
    {
        public PrincipalRepository() : this(new AnatoliDbContext()) { }
        public PrincipalRepository(DbContext context)
            : base(context)
        {
        }
    }
}
