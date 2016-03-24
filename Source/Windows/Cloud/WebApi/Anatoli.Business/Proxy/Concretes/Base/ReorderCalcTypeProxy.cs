using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.DataAccess.Models.Identity;
using Anatoli.ViewModels.BaseModels;

namespace Anatoli.Business.Proxy.Concretes.BaseConcretes
{
    public class ReorderCalcTypeProxy : AnatoliProxy<ReorderCalcType, ReorderCalcTypeViewModel>, IAnatoliProxy<ReorderCalcType, ReorderCalcTypeViewModel>
    {
        public override ReorderCalcTypeViewModel Convert(ReorderCalcType data)
        {
            return new ReorderCalcTypeViewModel
            {
                ID = data.Number_ID,
                UniqueId = data.Id,
                ApplicationOwnerId = data.ApplicationOwnerId,
                IsRemoved = data.IsRemoved,

                ReorderTypeName = data.ReorderTypeName,
            };
        }

        public override ReorderCalcType ReverseConvert(ReorderCalcTypeViewModel data)
        {
            return new ReorderCalcType
            {
                Number_ID = data.ID,
                Id = data.UniqueId,
                ApplicationOwnerId = data.ApplicationOwnerId,
                IsRemoved = data.IsRemoved,

                ReorderTypeName = data.ReorderTypeName,
            };
        }
    }
}