using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.DataAccess.Models.Identity;
using Anatoli.ViewModels.StockModels;
using Anatoli.ViewModels.BaseModels;

namespace Anatoli.Business.Proxy.Concretes.StockConcretes
{
    public class StockCompleteInfoProxy : AnatoliProxy<Stock, StockViewModel>, IAnatoliProxy<Stock, StockViewModel>
    {
        public IAnatoliProxy<StockActiveOnHand, StockActiveOnHandViewModel> ActiveOnHandProxy { get; set; }
        public IAnatoliProxy<StockProduct, StockProductViewModel> StockProductProxy { get; set; }
        #region Ctors
        public StockCompleteInfoProxy() :
            this(AnatoliProxy<StockActiveOnHand, StockActiveOnHandViewModel>.Create(), AnatoliProxy<StockProduct, StockProductViewModel>.Create()
            )
        { }

        public StockCompleteInfoProxy(IAnatoliProxy<StockActiveOnHand, StockActiveOnHandViewModel> activeOnHandProxy, IAnatoliProxy<StockProduct, StockProductViewModel> stockProductProxy
            )
        {
            ActiveOnHandProxy = activeOnHandProxy;
            StockProductProxy = stockProductProxy;
        }
        #endregion
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

            };
            
            if (data.StockOnHandSyncs.Count > 0)
            {

                var latestSyncDate = data.StockOnHandSyncs.Max(p => p.SyncDate);
                if (latestSyncDate != null)
                {
                    var latestSync = data.StockOnHandSyncs.First(f => f.SyncDate == latestSyncDate);
                    result.LatestStockOnHandSyncId = latestSync.Id;
                    result.StockActiveOnHand = ActiveOnHandProxy.Convert(latestSync.StockActiveOnHands.ToList());
                }
            }
            
            result.StockProduct = StockProductProxy.Convert(data.StockProducts.ToList());
            return result;

        }

        public override Stock ReverseConvert(StockViewModel data)
        {
            throw new NotImplementedException();
        }
    }
}