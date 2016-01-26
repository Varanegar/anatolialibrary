using System;
using Anatoli.DataAccess;
using System.Threading.Tasks;
using Anatoli.Business.Proxy;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.DataAccess.Models.Identity;
using Anatoli.DataAccess.Interfaces.Account;
using Anatoli.ViewModels.AuthorizationModels;
using Anatoli.DataAccess.Repositories.Account;
using System.Linq;

namespace Anatoli.Business.Domain.Authorization
{
    public class PermissionDomain : BusinessDomain<PermissionViewModel>, IBusinessDomain<Permission, PermissionViewModel>
    {
        #region Properties
        public IAnatoliProxy<Permission, PermissionViewModel> Proxy { get; set; }
        public IRepository<Permission> Repository { get; set; }
        #endregion

        #region Ctors
        PermissionDomain()
        { }
        public PermissionDomain(Guid privateLabelOwnerId) : this(privateLabelOwnerId, new AnatoliDbContext()) { }
        public PermissionDomain(Guid privateLabelOwnerId, AnatoliDbContext dbc)
            : this(new PermissionRepository(dbc), AnatoliProxy<Permission, PermissionViewModel>.Create())
        {
            PrivateLabelOwnerId = privateLabelOwnerId;
        }
        public PermissionDomain(IPermissionRepository permissionRepository, IAnatoliProxy<Permission, PermissionViewModel> proxy)
        {
            Proxy = proxy;
            Repository = permissionRepository;
        }
        #endregion

        #region Methods
        public Task<List<PermissionViewModel>> Delete(List<PermissionViewModel> ProductViewModels)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PermissionViewModel>> GetAll()
        {
            var data = await Repository.GetAllAsync();

            return Proxy.Convert(data.ToList());
        }

        public Task<List<PermissionViewModel>> GetAllChangedAfter(DateTime selectedDate)
        {
            throw new NotImplementedException();
        }

        public Task<List<PermissionViewModel>> PublishAsync(List<PermissionViewModel> ProductViewModels)
        {
            throw new NotImplementedException();
        }

        public Permission GetPermission(Guid permissionId)
        {
            var model = Repository.GetQuery().Where(p => p.Id == permissionId).FirstOrDefault();

            return model;
        }
        #endregion
    }
}