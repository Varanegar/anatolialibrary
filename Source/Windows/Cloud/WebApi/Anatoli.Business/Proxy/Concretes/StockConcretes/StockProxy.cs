using System.Linq;
using Anatoli.DataAccess.Models;
using Anatoli.ViewModels.StockModels;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.DataAccess.Models.Identity;
using System;

namespace Anatoli.Business.Proxy.Concretes.StockConcretes
{
    public class StockProxy : AnatoliProxy<Stock, StockViewModel>, IAnatoliProxy<Stock, StockViewModel>
    {
        public override StockViewModel Convert(Stock data)
        {
            var result = new StockViewModel
            {
                ID = data.Number_ID,
                UniqueId = data.Id,
                ApplicationOwnerId = data.ApplicationOwnerId,

                Accept1ById = data.Accept1ById,
                Accept2ById = data.Accept2ById,
                Accept3ById = data.Accept3ById,
                Address = data.Address,
                StockCode = data.StockCode,
                StockName = data.StockName,
                StockTypeId = data.StockTypeId,
                StoreId = data.StoreId,
                MainSCMStockId = data.MainSCMStock2Id,
                RelatedSCMStockId = data.RelatedSCMStock2Id,


                StockType = data.StockType == null ? new StockTypeViewModel() : new StockTypeViewModel { UniqueId = data.StockType.Id, StockTypeName = data.StockType.StockTypeName },

                MainStock = data.MainSCMStock2 == null ? new StockViewModel() : new StockViewModel { UniqueId = data.MainSCMStock2.Id, StockName = data.MainSCMStock2.StockName },
                RelatedStock = data.RelatedSCMStock2 == null ? new StockViewModel() : new StockViewModel { UniqueId = data.RelatedSCMStock2.Id, StockName = data.RelatedSCMStock2.StockName }
            };
            result.Approver1 = data.Accept1By == null ? new PricipalViewModel() : new PricipalViewModel { UniqueId = (Guid)data.Accept1ById, UserName = data.Accept1By.Title };
            result.Approver2 = data.Accept2By == null ? new PricipalViewModel() : new PricipalViewModel { UniqueId = (Guid)data.Accept2ById, UserName = data.Accept2By.Title };
            result.Approver3 = data.Accept3By == null ? new PricipalViewModel() : new PricipalViewModel { UniqueId = (Guid)data.Accept3ById, UserName = data.Accept3By.Title };

            if (data.StockOnHandSyncs.Count > 0)
            {
                var latestSyncDate = data.StockOnHandSyncs.Max(p => p.SyncDate);
                if (latestSyncDate != null)
                    result.LatestStockOnHandSyncId = data.StockOnHandSyncs.First(f => f.SyncDate == latestSyncDate).Id;
            }
            return result;

        }

        public override Stock ReverseConvert(StockViewModel data)
        {
            return new Stock
            {
                Number_ID = data.ID,
                Id = data.UniqueId,
                ApplicationOwnerId = data.ApplicationOwnerId,

                Accept1ById = data.Accept1ById,
                Accept2ById = data.Accept2ById,
                Accept3ById = data.Accept3ById,
                Address = data.Address,
                StockCode = data.StockCode,
                StockName = data.StockName,
                StockTypeId = data.StockTypeId,
                StoreId = data.StoreId,
                MainSCMStock2Id = data.MainSCMStockId,
                RelatedSCMStock2Id = data.RelatedSCMStockId,

            };
        }
    }
}