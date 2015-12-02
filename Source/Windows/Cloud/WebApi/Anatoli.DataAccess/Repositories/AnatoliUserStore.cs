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
        public Task CreateAsync(User user)
        {
            UserRepository.Add(user);

            UserRepository.SaveChanges();

            return Task.FromResult(0);
        }

        public Task DeleteAsync(User user)
        {
            UserRepository.Delete(user);

            UserRepository.SaveChanges();

            return Task.FromResult(0);
        }

        public Task<User> FindByIdAsync(Guid userId)
        {
            var model = UserRepository.GetById(userId);

            return Task<User>.FromResult(model);
        }

        public Task<User> FindByNameAsync(string userName)
        {
            var model = UserRepository.GetQuery().Where(p => p.UserName == userName).FirstOrDefault();

            return Task<User>.FromResult(model);
        }

        public Task UpdateAsync(User user)
        {
            UserRepository.EntryModified(user);

            UserRepository.SaveChanges();

            return Task.FromResult(0);
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
