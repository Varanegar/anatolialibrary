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
    public class AuthorizationDomain : BusinessDomainV2<PrincipalPermission, PrincipalPermissionViewModel, PrincipalPermissionRepository, IPrincipalPermissionRepository>
    {
        #region Properties
        public IRepository<User> UserRepository { get; set; }
        public IRepository<Permission> PermissionRepository { get; set; }
        #endregion

        #region Ctors
        public AuthorizationDomain(Guid applicationKey)
            : this(applicationKey, applicationKey, applicationKey) { }
        public AuthorizationDomain(Guid applicationKey, Guid dataOwnerKey, Guid dataOwnerCenterKey) 
            : this(applicationKey, dataOwnerKey, dataOwnerCenterKey, new AnatoliDbContext()) { }
        public AuthorizationDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey, AnatoliDbContext dbc)
            : base(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, dbc)
        {
            PermissionRepository = new PermissionRepository(dbc);
        }
        #endregion

        #region Methods
        public List<PrincipalPermission> GetPermissionsForPrincipal(string userId, string resource, string action)
        {
            //Todo: get all other related principal such as roles and groups

            //var model = MainRepository.GetQuery().Where(p => p.UserId == userId &&
            //                                            p.Permission.Resource == resource &&
            //                                            p.Permission.Action == action)
            //                                     .ToList();

            //return model;
            return null;
        }

        public async Task<ICollection<PrincipalPermission>> GetPermissionsForPrincipal(string principalId)
        {
            //Todo: get all other related principal such as roles and groups
            //var model = await MainRepository.FindAllAsync(p => p.UserId == principalId);

            //return model;
            return null;
        }

        public async Task SavePermissions(List<PrincipalPermission> pp, string principalId)
        {
            try
            {
                //await MainRepository.DeleteBatchAsync(p => p.UserId == principalId);

                foreach (var item in pp)
                    await MainRepository.AddAsync(item);

                await MainRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        public async Task<ICollection<Permission>> GetAllPermissions()
        {
            var data = await PermissionRepository.GetAllAsync();

            return data;
        }
        #endregion
    }
}
