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
        public Guid DataOwnerKey { get; protected set; }
        public virtual UserRepository UserRepository { get; set; }
        public virtual PrincipalRepository PrincipalRepository { get; set; }
        public AnatoliDbContext DBContext { get; set; }
        #endregion

        #region Ctors
        public UserDomain(Guid applicationOwnerKey, Guid dataOwnerKey)
            : this(applicationOwnerKey, dataOwnerKey, new AnatoliDbContext())
        {

        }
        public UserDomain(Guid applicationOwnerKey, Guid dataOwnerKey, AnatoliDbContext dbc)
        {
            UserRepository = new UserRepository(dbc);
            PrincipalRepository = new PrincipalRepository(dbc);
            ApplicationOwnerKey = applicationOwnerKey;
            DataOwnerKey = dataOwnerKey;
            Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        }
        #endregion

        #region Methods
        public async Task<User> GetByUsernameAsync(string username)
        {
            return await UserRepository.FindAsync(p => p.UserNameStr == username && p.ApplicationOwnerId == ApplicationOwnerKey && DataOwnerKey == p.DataOwnerId);
        }

        public async Task<User> GetByIdAsync(string userId)
        {
            return await UserRepository.FindAsync(p => p.Id == userId && p.ApplicationOwnerId == ApplicationOwnerKey && DataOwnerKey == p.DataOwnerId);
        }

        public async Task<User> GetByPhoneAsync(string phone)
        {
            return await UserRepository.FindAsync(p => p.PhoneNumber == phone && p.ApplicationOwnerId == ApplicationOwnerKey && DataOwnerKey == p.DataOwnerId);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await UserRepository.FindAsync(p => p.Email == email && p.ApplicationOwnerId == ApplicationOwnerKey && DataOwnerKey == p.DataOwnerId);
        }

        public async Task<User> UserExists(string email, string phone, string username)
        {
            return await UserRepository.FindAsync(p => (p.Email == email || p.PhoneNumber == phone || p.UserNameStr == username) && p.ApplicationOwnerId == ApplicationOwnerKey && p.DataOwnerId == DataOwnerKey);
        }

        public async Task<User> UserExists(string usernameOrEmailOrPhone)
        {
            return await UserRepository.FindAsync(p => (p.Email == usernameOrEmailOrPhone || p.PhoneNumber == usernameOrEmailOrPhone || p.UserNameStr == usernameOrEmailOrPhone) && p.ApplicationOwnerId == ApplicationOwnerKey && p.DataOwnerId == DataOwnerKey);
        }

        public async Task SavePerincipal(Principal principal)
        {
            try
            {
                await PrincipalRepository.AddAsync(principal);

                await PrincipalRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }


        #endregion
    }
}
