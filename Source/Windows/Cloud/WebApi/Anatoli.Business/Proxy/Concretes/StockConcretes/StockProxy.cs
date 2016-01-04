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
    public class StockProxy : AnatoliProxy<Stock, StockViewModel>, IAnatoliProxy<Stock, StockViewModel>
    {
        public override StockViewModel Convert(Stock data)
        {
            return new StockViewModel
            {
                ID = data.Number_ID,
                UniqueId = data.Id,
                PrivateOwnerId = data.PrivateLabelOwner.Id,

                Accept1ById = data.Accept1ById,
                Accept2ById = data.Accept2ById,
                Accept3ById = data.Accept3ById,
                Address = data.Address,
                StockCode = data.StockCode,
                StockName = data.StockName,
                StockTypeId = data.StockTypeId,
                StoreId = data.StoreId,


            };
        }

        public override Stock ReverseConvert(StockViewModel data)
        {
            return new Stock
            {
                Number_ID = data.ID,
                Id = data.UniqueId,
                PrivateLabelOwner = new Principal { Id = data.PrivateOwnerId },

                Accept1ById = data.Accept1ById,
                Accept2ById = data.Accept2ById,
                Accept3ById = data.Accept3ById,
                Address = data.Address,
                StockCode = data.StockCode,
                StockName = data.StockName,
                StockTypeId = data.StockTypeId,
                StoreId = data.StoreId,            
            };
        }
    }
}