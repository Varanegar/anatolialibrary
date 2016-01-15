using System;
using System.Linq;
using System.Data.Entity;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;

namespace Anatoli.DataAccess.Repositories
{
    public class CustomerShipAddressRepository : AnatoliRepository<CustomerShipAddress>, ICustomerShipAddressRepository
    {
        #region Ctors
        public CustomerShipAddressRepository() : this(new AnatoliDbContext()) { }
        public CustomerShipAddressRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion
    }
}
