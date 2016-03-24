using System;
using System.Linq;
using Anatoli.Business.Proxy;
using System.Threading.Tasks;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;
using Anatoli.DataAccess.Repositories;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.DataAccess;
using Anatoli.ViewModels.ProductModels;
using Anatoli.ViewModels.StockModels;
using Anatoli.ViewModels.BaseModels;
using Anatoli.DataAccess.Models.Identity;

namespace Anatoli.Business.Domain
{
    public class UserDomain 
    {
        #region Properties
        protected static log4net.ILog Logger { get; set; }
        public Guid ApplicationOwnerKey { get; protected set; }
        public virtual UserRepository UserRepository { get; set; }
        public AnatoliDbContext DBContext { get; set; }
        #endregion

        #region Ctors
        public UserDomain(Guid applicationOwnerKey)
            : this(applicationOwnerKey, new AnatoliDbContext())
        {

        }
        public UserDomain(Guid applicationOwnerKey, AnatoliDbContext dbc)
        {
            UserRepository = new UserRepository(dbc);
            ApplicationOwnerKey = applicationOwnerKey;
            Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        }
        #endregion

        #region Methods
        public async Task<User> GetByUsernameAsync(string username)
        {
            return await UserRepository.FindAsync(p => p.UserName == username && p.ApplicationOwnerId == ApplicationOwnerKey);
        }

        public async Task<User> GetByIdAsync(Guid userId)
        {
            return await UserRepository.GetByIdAsync(userId);
        }

        public async Task<User> GetByPhoneAsync(string phone)
        {
            return await UserRepository.FindAsync(p => p.PhoneNumber == phone && p.ApplicationOwnerId == ApplicationOwnerKey);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await UserRepository.FindAsync(p => p.Email == email && p.ApplicationOwnerId == ApplicationOwnerKey);
        }

        public async Task<User> UserExists(string email, string phone, string username)
        {
            return await UserRepository.FindAsync(p => (p.Email == email || p.PhoneNumber == phone || p.UserName == username ) && p.ApplicationOwnerId == ApplicationOwnerKey);
        }

        #endregion
    }
}
