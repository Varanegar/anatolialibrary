using System;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.DataAccess.Models.Identity;
using Anatoli.ViewModels.AuthorizationModels;

namespace Anatoli.Business.Proxy.Concretes.AuthorizationProxies
{
    //public class PermissionProxy : AnatoliProxy<Permission, PermissionViewModel>, IAnatoliProxy<Permission, PermissionViewModel>
    //{
    //    public override PermissionViewModel Convert(Permission data)
    //    {
    //        return new PermissionViewModel
    //        {
    //            Id = data.Id,
    //            //Resource = data.Resource,
    //            //Action = data.Action,
    //            //Title = data.PersianTitle,
    //        };
    //    }

    //    public override Permission ReverseConvert(PermissionViewModel data)
    //    {
    //        return new Permission
    //        {
    //            Id = data.UniqueId,
    //            //Number_ID = data.ID,
    //            //Resource = data.Resource,
    //            //Action = data.Action,
    //            //PersianTitle = data.Title
    //        };
    //    }

    //}    
}
