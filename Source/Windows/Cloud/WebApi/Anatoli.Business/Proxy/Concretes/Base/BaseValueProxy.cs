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
    public class BaseValueProxy : AnatoliProxy<BaseValue, BaseValueViewModel>, IAnatoliProxy<BaseValue, BaseValueViewModel>
    {
        public override BaseValueViewModel Convert(BaseValue data)
        {
            return new BaseValueViewModel
            {
                ID = data.Number_ID,
                UniqueId = data.Id,
                ApplicationOwnerId = data.ApplicationOwnerId,
                BaseValueName = data.BaseValueName,
            };
        }

        public override BaseValue ReverseConvert(BaseValueViewModel data)
        {
            return new BaseValue
            {
                Number_ID = data.ID,
                Id = data.UniqueId,
                BaseValueName = data.BaseValueName,

                ApplicationOwnerId = data.ApplicationOwnerId,
            };
        }
    }
}