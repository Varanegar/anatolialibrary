using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.DataAccess.Models.Identity;
using Anatoli.ViewModels.BaseModels;

namespace Anatoli.Business.Proxy.Concretes.BaseConcretes
{
    public class FiscalYearProxy : AnatoliProxy<FiscalYear, FiscalYearViewModel>, IAnatoliProxy<FiscalYear, FiscalYearViewModel>
    {
        public override FiscalYearViewModel Convert(FiscalYear data)
        {
            return new FiscalYearViewModel
            {
                ID = data.Number_ID,
                UniqueId = data.Id,
                PrivateOwnerId = data.PrivateLabelOwner_Id,

                FromDate = data.FromDate,
                FromPDate = data.FromPdate,
                ToDate = data.ToDate,
                ToPDate = data.ToPdate,
                IsRemoved = data.IsRemoved,


            };
        }

        public override FiscalYear ReverseConvert(FiscalYearViewModel data)
        {
            return new FiscalYear
            {
                Number_ID = data.ID,
                Id = data.UniqueId,
                PrivateLabelOwner = new Principal { Id = data.PrivateOwnerId },
                IsRemoved = data.IsRemoved,

                FromDate = data.FromDate,
                FromPdate = data.FromPDate,
                ToDate = data.ToDate,
                ToPdate = data.ToPDate,
            
            };
        }
    }
}