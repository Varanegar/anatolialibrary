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
    
    public class AuthorizationDomain //: BusinessDomainV2<PrincipalPermission, PrincipalPermissionViewModel, PrincipalPermissionRepository, IPrincipalPermissionRepository>
    {
        #region Properties
        protected static log4net.ILog Logger { get; set; }
        public IRepository<Principal> PrincipalRepository { get; set; }
        public IRepository<Permission> PermissionRepository { get; set; }
        public AnatoliDbContext DBContext { get; set; }
        public IRepository<PrincipalPermission> MainRepository { get; set; }
        public Guid ApplicationOwnerKey { get; protected set; }
        public Guid DataOwnerKey { get; protected set; }
        public Guid DataOwnerCenterKey { get; protected set; }
        #endregion

        #region Ctors
        public AuthorizationDomain(Guid applicationKey)
            : this(applicationKey, applicationKey, applicationKey) { }
        public AuthorizationDomain(Guid applicationKey, Guid dataOwnerKey, Guid dataOwnerCenterKey) 
            : this(applicationKey, dataOwnerKey, dataOwnerCenterKey, new AnatoliDbContext()) { }
        public AuthorizationDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey, AnatoliDbContext dbc)
        {
            ApplicationOwnerKey = applicationOwnerKey; DataOwnerKey = dataOwnerKey; DataOwnerCenterKey = dataOwnerCenterKey;

            PermissionRepository = new PermissionRepository(dbc);
            MainRepository = new PrincipalPermissionRepository(dbc);
            PrincipalRepository = new PrincipalRepository(dbc); 
            DBContext = dbc;

        }
        #endregion

        #region Methods
        public List<PrincipalPermission> GetPermissionsForPrincipal(string principalId, string resource, string action)
        {
            //Todo: get all other related principal such as roles and groups

            var model = MainRepository.GetQuery().Where(p => p.PrincipalId == Guid.Parse(principalId) &&
                                                        p.Permission.ApplicationModuleResource.Name == resource &&
                                                        p.Permission.PermissionAction.Name == action)
                                                 .ToList();

            return model;
        }

        public async Task<ICollection<PrincipalPermissionViewModel>> GetPermissionsForPrincipal(string principalId)
        {
            //Todo: get all other related principal such as roles and groups
            var model = new List<PrincipalPermissionViewModel>();
            await Task.Factory.StartNew(() =>
            {
                var query = from pp in DBContext.PrincipalPermissions
                            join p in DBContext.Permissions on pp.Permission_Id equals p.Id
                            join r in DBContext.ApplicationModuleResources on p.ApplicationModuleResourceId equals r.Id
                            join a in DBContext.PermissionActions on p.PermissionActionId equals a.Id
                            where pp.PrincipalId == Guid.Parse(principalId)
                            select new PrincipalPermissionViewModel
                            {
                                PrincipalId = principalId,
                                Resource = r.Name,
                                Action = a.Name,
                                Grant = (pp.Grant == 1) ? true : false,
                                PermissionId = pp.Id
                            };
                model = query.ToList();

            });

            return model;
        }

        public async Task SavePermissions(List<PrincipalPermission> pp, string principalId)
        {
            try
            {
                await MainRepository.DeleteBatchAsync(p => p.PrincipalId == Guid.Parse(principalId));

                foreach (var item in pp)
                    await MainRepository.AddAsync(item);

                await MainRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        public async Task<ICollection<PermissionViewModel>> GetAllPermissions()
        {
            var model = new List<PermissionViewModel>();
            await Task.Factory.StartNew(() =>
            {
                var query = from p in DBContext.Permissions 
                            join r in DBContext.ApplicationModuleResources on p.ApplicationModuleResourceId equals r.Id
                            join am in DBContext.ApplicationModules on r.ApplicationModuleId equals am.Id
                            join apr in DBContext.ApplicationOwners on am.ApplicationId equals apr.ApplicationId
                            join a in DBContext.PermissionActions on p.PermissionActionId equals a.Id
                            where apr.Id == ApplicationOwnerKey 
                            select new PermissionViewModel
                            {
                                Id = p.Id,
                                Resource = r.Name,
                                Action = a.Name,
                                Title = p.Name
                            };
                model = query.ToList();

            });

            MainRepository.DeleteBatch(p => p.Permission.PermissionAction.Name == "save");
            return model;
        }
        #endregion
    }
}
