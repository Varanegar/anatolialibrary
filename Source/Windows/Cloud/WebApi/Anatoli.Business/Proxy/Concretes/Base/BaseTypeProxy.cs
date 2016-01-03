using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.DataAccess.Models.Identity;
using Anatoli.ViewModels.ProductModels;
using Anatoli.ViewModels.BaseModels;

namespace Anatoli.Business.Proxy.BaseConcretes
{
    public class BaseTypeProxy : AnatoliProxy<BaseType, BaseTypeViewModel>, IAnatoliProxy<BaseType, BaseTypeViewModel>
    {
        public IAnatoliProxy<BaseValue, BaseValueViewModel> BaseValueProxy { get; set; }

        #region Ctors
        public BaseTypeProxy() :
            this(AnatoliProxy<BaseValue, BaseValueViewModel>.Create()
            )
        { }

        public BaseTypeProxy(IAnatoliProxy<BaseValue, BaseValueViewModel> baseValueProxy
            )
        {
            BaseValueProxy = baseValueProxy;
        }
        #endregion

        public override BaseTypeViewModel Convert(BaseType data)
        {
            return new BaseTypeViewModel
            {
                ID = data.Number_ID,
                UniqueId = data.Id,
                PrivateOwnerId = data.PrivateLabelOwner.Id,
                BaseTypeDesc = data.BaseTypeDesc,

                BaseValues = BaseValueProxy.Convert(data.BaseValues.ToList()),
            };
        }

        public override BaseType ReverseConvert(BaseTypeViewModel data)
        {
            return new BaseType
            {
                Number_ID = data.ID,
                Id = data.UniqueId,

                BaseTypeDesc = data.BaseTypeDesc,

                PrivateLabelOwner = new Principal { Id = data.PrivateOwnerId },

                BaseValues = data.BaseValues != null? BaseValueProxy.ReverseConvert(data.BaseValues.ToList()) : null,
            };
        }
    }
}