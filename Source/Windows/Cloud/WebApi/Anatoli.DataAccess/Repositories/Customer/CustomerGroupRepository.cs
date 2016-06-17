using System;
using System.Linq;
using System.Data.Entity;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;
using Anatoli.Common.DataAccess.Repositories;

namespace Anatoli.DataAccess.Repositories
{
    public class CustomerGroupRepository : AnatoliRepository<CustomerGroup>, ICustomerGroupRepository
    {
        #region Ctors
        public CustomerGroupRepository() : this(new AnatoliDbContext()) { }
        public CustomerGroupRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion
    }
}
