using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.DataAccess.Models.Identity;
using Anatoli.ViewModels.BaseModels;
using Anatoli.ViewModels.StockModels;

namespace Anatoli.Business.Proxy.Concretes.StockHistoryOnHandConcretes
{
    public class StockHistoryOnHandProxy : AnatoliProxy<StockHistoryOnHand, StockHistoryOnHandViewModel>, IAnatoliProxy<StockHistoryOnHand, StockHistoryOnHandViewModel>
    {
        public override StockHistoryOnHandViewModel Convert(StockHistoryOnHand data)
        {
            return new StockHistoryOnHandViewModel
            {
                ID = data.Number_ID,
                UniqueId = data.Id,
                PrivateOwnerId = data.PrivateLabelOwner.Id,

            };
        }

        public override StockHistoryOnHand ReverseConvert(StockHistoryOnHandViewModel data)
        {
            return new StockHistoryOnHand
            {
                Number_ID = data.ID,
                Id = data.UniqueId,
                PrivateLabelOwner = new Principal { Id = data.PrivateOwnerId },

            
            };
        }
    }
}