using System;
using System.Linq;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using Anatoli.DataAccess.Models.Identity;
using System.Threading.Tasks;

namespace Anatoli.DataAccess.Repositories
{
    public class AnatoliUserStore : IUserStore<User, Guid>, IUserPasswordStore<User, Guid>, IDisposable
    {
        #region Properties
        UserRepository UserRepository { get; set; }
        #endregion

        #region Ctors
        public AnatoliUserStore()
            : this(new UserRepository(new AnatoliDbContext()))
        { }
        public AnatoliUserStore(UserRepository userRepository)
        {
            UserRepository = userRepository;
        }
        #endregion

        #region User Store
        public async Task CreateAsync(User user)
        {
          await  UserRepository.AddAsync(user);

          await UserRepository.SaveChangesAsync();            
        }

        public async Task DeleteAsync(User user)
        {
            await UserRepository.DeleteAsync(user);

            await UserRepository.SaveChangesAsync();
        }

        public Task<User> FindByIdAsync(Guid userId)
        {
            return UserRepository.GetByIdAsync(userId);
        }

        public Task<User> FindByNameAsync(string userName)
        {
            var model = UserRepository.GetQuery().Where(p => p.UserName == userName).FirstOrDefault();

            return Task<User>.FromResult(model);
        }

        public async Task UpdateAsync(User user)
        {
            UserRepository.EntryModified(user);

            await UserRepository.SaveChangesAsync();                       
        }
        #endregion

        #region User Password Store
        public Task<string> GetPasswordHashAsync(User user)
        {
            return Task<string>.FromResult(user.Password);
        }

        public Task<bool> HasPasswordAsync(User user)
        {
            return Task<bool>.FromResult(string.IsNullOrEmpty(user.Password));
        }

        public Task SetPasswordHashAsync(User user, string passwordHash)
        {
            user.Password = passwordHash;

            return Task.FromResult(0);
        }
        #endregion

        public void Dispose()
        {
            UserRepository.Dispose();

            UserRepository = null;
        }
    }
}
