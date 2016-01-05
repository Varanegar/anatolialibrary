using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.DataAccess.Models.Identity;
using Anatoli.ViewModels.BaseModels;
using Anatoli.ViewModels.StockModels;

namespace Anatoli.Business.Proxy.Concretes.StockProductRequestStatusConcretes
{
    public class StockProductRequestStatusProxy : AnatoliProxy<StockProductRequestStatus, StockProductRequestStatusViewModel>, IAnatoliProxy<StockProductRequestStatus, StockProductRequestStatusViewModel>
    {
        public override StockProductRequestStatusViewModel Convert(StockProductRequestStatus data)
        {
            return new StockProductRequestStatusViewModel
            {
                ID = data.Number_ID,
                UniqueId = data.Id,
                PrivateOwnerId = data.PrivateLabelOwner.Id,

                StockProductRequestStatusName = data.StockProductRequestStatusName,
            };
        }

        public override StockProductRequestStatus ReverseConvert(StockProductRequestStatusViewModel data)
        {
            return new StockProductRequestStatus
            {
                Number_ID = data.ID,
                Id = data.UniqueId,
                PrivateLabelOwner = new Principal { Id = data.PrivateOwnerId },

                StockProductRequestStatusName = data.StockProductRequestStatusName,

            
            };
        }
    }
}