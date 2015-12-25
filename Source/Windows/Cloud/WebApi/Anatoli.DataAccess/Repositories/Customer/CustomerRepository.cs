using System;
using System.Linq;
using System.Data.Entity;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;

namespace Anatoli.DataAccess.Repositories
{
    public class CustomerRepository : AnatoliRepository<Customer>, ICustomerRepository
    {
        #region Ctors
        public CustomerRepository() : this(new AnatoliDbContext()) { }
        public CustomerRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion
    }
}
