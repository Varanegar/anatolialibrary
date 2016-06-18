using Anatoli.DataAccess.Models;
using Anatoli.DataAccess.Interfaces;
using Anatoli.Common.DataAccess.Repositories;

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
