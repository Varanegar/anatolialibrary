using Anatoli.DataAccess.Models;
using Anatoli.ViewModels.StockModels;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.DataAccess.Models.Identity;

namespace Anatoli.Business.Proxy.Concretes.StockTypeConcretes
{
    public class StockTypeProxy : AnatoliProxy<StockType, StockTypeViewModel>, IAnatoliProxy<StockType, StockTypeViewModel>
    {
        public override StockTypeViewModel Convert(StockType data)
        {
            return new StockTypeViewModel
            {
                ID = data.Number_ID,
                UniqueId = data.Id,
                ApplicationOwnerId = data.ApplicationOwnerId,
                StockTypeName = data.StockTypeName
            };
        }

        public override StockType ReverseConvert(StockTypeViewModel data)
        {
            return new StockType
            {
                Number_ID = data.ID,
                Id = data.UniqueId,
                ApplicationOwnerId = data.ApplicationOwnerId,
                StockTypeName = data.StockTypeName
            };
        }
    }
}