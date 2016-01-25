using System;
using System.Linq;
using Anatoli.DataAccess;
using System.Threading.Tasks;
using Anatoli.Business.Proxy;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;
using Anatoli.DataAccess.Repositories;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.DataAccess.Models.Identity;
using Anatoli.DataAccess.Interfaces.Account;
using Anatoli.ViewModels.AuthorizationModels;
using Anatoli.DataAccess.Repositories.Account;
using Anatoli.Business.Proxy.Concretes.AuthorizationProxies;

namespace Anatoli.Business.Domain.Authorization
{
    public class AuthorizationDomain : BusinessDomain<PrincipalPermissionViewModel>, IBusinessDomain<PrincipalPermission, PrincipalPermissionViewModel>
    {
        #region Properties
        public IAnatoliProxy<PrincipalPermission, PrincipalPermissionViewModel> Proxy { get; set; }
        public IRepository<PrincipalPermission> Repository { get; set; }
        public IRepository<User> UserRepository { get; set; }
        protected Principal Owner
        {
            get
            {
                return UserRepository.GetQuery().Where(p => p.Principal.Id == PrivateLabelOwnerId).Select(s => s.Principal).First();
            }
        }


        public AnatoliDbContext DBContext { get; set; }
        #endregion

        #region Ctors
        AuthorizationDomain()
        { }
        public AuthorizationDomain(Guid privateLabelOwnerId) : this(privateLabelOwnerId, new AnatoliDbContext()) { }
        public AuthorizationDomain(Guid privateLabelOwnerId, AnatoliDbContext dbc)
            : this(new PrincipalPermissionRepository(dbc), new UserRepository(dbc),
                  AnatoliProxy<PrincipalPermission, PrincipalPermissionViewModel>.Create())
        {
            PrivateLabelOwnerId = privateLabelOwnerId;
            DBContext = dbc;
        }
        public AuthorizationDomain(IPrincipalPermissionRepository principalPermissionRepository, IUserRepository usersRepository,
                                   IAnatoliProxy<PrincipalPermission, PrincipalPermissionViewModel> proxy)
        {
            Proxy = proxy;
            Repository = principalPermissionRepository;
            UserRepository = usersRepository;
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
            var user = UserRepository.GetQuery().Where(p => p.Id == userId).FirstOrDefault();

            var model = Repository.GetQuery().Where(p => p.Principal.Id == user.Principal.Id &&
                                                    p.Permission.Resource == resource &&
                                                    p.Permission.Action == action)
                                             .ToList();

            return Proxy.Convert(model);
        }

        public async Task SavePermissions(List<PrincipalPermission> pp, Guid principalId)
        {
            try
            {
                var pr = new PrincipalRepository(DBContext);

                var old_pricipalPermissions = Repository.GetQuery().Where(p => p.Principal.Id == principalId).ToList();

                await Repository.DeleteRangeAsync(old_pricipalPermissions);
                old_pricipalPermissions.Clear();

                var principal = pr.GetQuery().Where(p => p.Id == principalId).First();

                var permissionDomain = new PermissionDomain(PrivateLabelOwnerId, DBContext);
                pp.ForEach(itm =>
                {
                    itm.Id = Guid.NewGuid();

                    itm.Permission = permissionDomain.GetPermission(itm.Permission.Id);

                    itm.Principal = principal;

                    itm.PrivateLabelOwner = Owner;

                    Repository.Add(itm);
                });

                await Repository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
            }
        }

        public List<PrincipalPermissionViewModel> GetPermissionsForPrincipal(Guid principalId )
        {
            //Todo: get all other related principal such as roles and groups
            var model = Repository.GetQuery().Where(p => p.Principal.Id == principalId)
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
