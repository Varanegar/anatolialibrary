using Anatoli.DataAccess.Models;
using Anatoli.DataAccess.Interfaces;
using Anatoli.Common.DataAccess.Repositories;

namespace Anatoli.DataAccess.Repositories
{
    public class BaseValueRepository : AnatoliRepository<BaseValue>, IBaseValueRepository
    {
        #region Ctors
        public BaseValueRepository() : this(new AnatoliDbContext()) { }
        public BaseValueRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion
    }
}
