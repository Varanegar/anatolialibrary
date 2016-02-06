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
    public class AuthorizationDomain : BusinessDomain1<PrincipalPermission>, IBusinessDomain1<PrincipalPermission>
    {
        #region Properties
        public override IRepository<PrincipalPermission> MainRepository { get; set; }
        public IRepository<User> UserRepository { get; set; }
        public IRepository<Permission> PermissionRepository { get; set; }
        public AnatoliDbContext DBContext { get; set; }
        #endregion

        #region Ctors
        AuthorizationDomain() { }
        public AuthorizationDomain(Guid applicationKey) : this(applicationKey, new AnatoliDbContext()) { }
        AuthorizationDomain(Guid applicationKey, AnatoliDbContext dbc)
            : this(new PrincipalPermissionRepository(dbc), new UserRepository(dbc), new PermissionRepository(dbc))
        {
            ApplicationKey = applicationKey;
            DBContext = dbc;
        }
        public AuthorizationDomain(IPrincipalPermissionRepository principalPermissionRepository,
                                   IUserRepository usersRepository, IPermissionRepository permissionRepository)
        {
            MainRepository = principalPermissionRepository;
            UserRepository = usersRepository;
            PermissionRepository = permissionRepository;
        }
        #endregion

        #region Methods
        public List<PrincipalPermission> GetPermissionsForPrincipal(string userId, string resource, string action)
        {
            //Todo: get all other related principal such as roles and groups
            var _userId = Guid.Parse(userId);

            var model = MainRepository.GetQuery().Where(p => p.Principal.Id == _userId &&
                                                        p.Permission.Resource == resource &&
                                                        p.Permission.Action == action)
                                                 .ToList();

            return model;
        }

        public async Task<ICollection<PrincipalPermission>> GetPermissionsForPrincipal(Guid principalId)
        {
            //Todo: get all other related principal such as roles and groups
            var model = await MainRepository.FindAllAsync(p => p.Principal.Id == principalId);

            return model;
        }

        public async Task SavePermissions(List<PrincipalPermission> pp, Guid principalId)
        {
            try
            {
                await MainRepository.DeleteBatchAsync(p => p.Principal.Id == principalId);

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
