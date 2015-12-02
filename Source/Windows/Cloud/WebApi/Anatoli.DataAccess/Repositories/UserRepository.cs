using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;
using Anatoli.DataAccess.Models.Identity;

namespace Anatoli.DataAccess.Repositories
{
    public class UserRepository : AnatoliRepository<User>, IUserRepository
    {
        public UserRepository(DbContext context)
            : base(context)
        {
        }

        //notice: new custom methods could be added in here
    }
}
