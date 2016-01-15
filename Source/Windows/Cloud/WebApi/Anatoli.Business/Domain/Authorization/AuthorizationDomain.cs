using System;
using System.Linq;
using Anatoli.DataAccess;
using System.Threading.Tasks;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;
using Anatoli.DataAccess.Repositories;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.DataAccess.Models.Identity;
using Anatoli.ViewModels.AuthorizationModels;
using Anatoli.DataAccess.Repositories.Account;
using Anatoli.DataAccess.Interfaces.Account;
using Anatoli.Business.Proxy;
using Anatoli.Business.Proxy.Concretes.AuthorizationProxies;

namespace Anatoli.Business.Domain.Authorization
{
    public class AuthorizationDomain : BusinessDomain<PrincipalPermissionViewModel>, IBusinessDomain<PrincipalPermission, PrincipalPermissionViewModel>
    {
        #region Properties
        public IAnatoliProxy<PrincipalPermission, PrincipalPermissionViewModel> Proxy { get; set; }
        public IRepository<PrincipalPermission> Repository { get; set; }
        public IRepository<User> UsersRepository { get; set; }
        #endregion

        #region Ctors
        AuthorizationDomain()
        { }
        public AuthorizationDomain(Guid privateLabelOwnerId) : this(privateLabelOwnerId, new AnatoliDbContext()) { }
        public AuthorizationDomain(Guid privateLabelOwnerId, AnatoliDbContext dbc)
            : this(new PrincipalPermissionRepository(dbc), new UserRepository(dbc), AnatoliProxy<PrincipalPermission, PrincipalPermissionViewModel>.Create(typeof(PrincipalPermissionProxy).FullName))
        {
            PrivateLabelOwnerId = privateLabelOwnerId;
        }
        public AuthorizationDomain(IPrincipalPermissionRepository principalPermissionRepository, IUserRepository usersRepository, IAnatoliProxy<PrincipalPermission, PrincipalPermissionViewModel> proxy)
        {
            Proxy = proxy;
            Repository = principalPermissionRepository;
            UsersRepository = usersRepository;
        }
        #endregion

        #region Methods
        public Task<List<PrincipalPermissionViewModel>> Delete(List<PrincipalPermissionViewModel> ProductViewModels)
        {
            throw new NotImplementedException();
        }

        public Task<List<PrincipalPermissionViewModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<PrincipalPermissionViewModel> GetPermissionsForPrincipal(string userId, string resource, string action)
        {
            //Todo: get all other related principal such as roles and groups
            var user = UsersRepository.GetQuery().Where(p => p.Id == userId).FirstOrDefault();

            var model = Repository.GetQuery().Where(p => p.Principal.Id == user.Principal.Id &&
                                                    p.Permission.Resource == resource &&
                                                    p.Permission.Action == action)
                                             .ToList();

            return Proxy.Convert(model);
        }

        public Task<List<PrincipalPermissionViewModel>> GetAllChangedAfter(DateTime selectedDate)
        {
            throw new NotImplementedException();
        }

        public Task<List<PrincipalPermissionViewModel>> PublishAsync(List<PrincipalPermissionViewModel> ProductViewModels)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
