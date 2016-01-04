using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.DataAccess.Models.Identity;
using Anatoli.ViewModels.BaseModels;
using Anatoli.ViewModels.StockModels;

namespace Anatoli.Business.Proxy.Concretes.ProductConcretes
{
    public class ReorderCalcTypeProxy : AnatoliProxy<ReorderCalcType, ReorderCalcTypeViewModel>, IAnatoliProxy<ReorderCalcType, ReorderCalcTypeViewModel>
    {
        public override ReorderCalcTypeViewModel Convert(ReorderCalcType data)
        {
            return new ReorderCalcTypeViewModel
            {
                ID = data.Number_ID,
                UniqueId = data.Id,
                PrivateOwnerId = data.PrivateLabelOwner.Id,

            };
        }

        public override ReorderCalcType ReverseConvert(ReorderCalcTypeViewModel data)
        {
            return new ReorderCalcType
            {
                Number_ID = data.ID,
                Id = data.UniqueId,
                PrivateLabelOwner = new Principal { Id = data.PrivateOwnerId },

            
            };
        }
    }
}