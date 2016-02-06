using System;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.DataAccess.Models.Identity;
using Anatoli.ViewModels.AuthorizationModels;

namespace Anatoli.Business.Proxy.Concretes.AuthorizationProxies
{
    public class PrincipalPermissionProxy : AnatoliProxy<PrincipalPermission, PrincipalPermissionViewModel>, IAnatoliProxy<PrincipalPermission, PrincipalPermissionViewModel>
    {
        public override PrincipalPermissionViewModel Convert(PrincipalPermission data)
        {
            return new PrincipalPermissionViewModel
            {
                UniqueId = data.Id,
                ID = data.Number_ID,
                PrincipalId = data.Principal_Id,
                PermissionId = data.Permission_Id,
                Resource = data.Permission.Resource,
                Action = data.Permission.Action,
                Grant = data.Grant
            };
        }

        public override PrincipalPermission ReverseConvert(PrincipalPermissionViewModel data)
        {
            return new PrincipalPermission
            {
                Id = data.UniqueId,
                Number_ID = data.ID,
                Principal_Id = data.PrincipalId,
                Permission_Id = data.PermissionId,
                Grant = data.Grant
            };
        }
    }
}