using Anatoli.DataAccess.Models;
using Anatoli.DataAccess.Interfaces;
using Anatoli.Common.DataAccess.Repositories;

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
