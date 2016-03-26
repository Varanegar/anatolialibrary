using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.DataAccess.Models.Identity;
using Anatoli.ViewModels.BaseModels;
using Anatoli.ViewModels.StockModels;

namespace Anatoli.Business.Proxy.Concretes.StockOnHandSyncConcretes
{
    public class StockOnHandSyncProxy : AnatoliProxy<StockOnHandSync, StockOnHandSyncViewModel>, IAnatoliProxy<StockOnHandSync, StockOnHandSyncViewModel>
    {
        public override StockOnHandSyncViewModel Convert(StockOnHandSync data)
        {
            return new StockOnHandSyncViewModel
            {
                ID = data.Number_ID,
                UniqueId = data.Id,
                ApplicationOwnerId = data.ApplicationOwnerId,

            };
        }

        public override StockOnHandSync ReverseConvert(StockOnHandSyncViewModel data)
        {
            return new StockOnHandSync
            {
                Number_ID = data.ID,
                Id = data.UniqueId,
                ApplicationOwnerId = data.ApplicationOwnerId,

            
            };
        }
    }
}