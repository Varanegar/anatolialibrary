using Anatoli.PMC.ViewModels.Order;
using Anatoli.ViewModels.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thunderstruck;

namespace Anatoli.PMC.DataAccess.DataAdapter
{
    public class SellAdapter : BaseAdapter
    {
        public static void SavePurchaseOrder(PMCSellViewModel orderInfo)
        {
            using (var context = new DataContext(Transaction.Begin))
            {
                DataObject<PMCSellViewModel> sellDataObject = new DataObject<PMCSellViewModel>("Sell");
                sellDataObject.Insert(orderInfo, context);
                DataObject<PMCSellDetailViewModel> lineItemDataObject = new DataObject<PMCSellDetailViewModel>("SellDetail");
                orderInfo.SellDetail.ForEach(item =>
                {
                    lineItemDataObject.Insert(item, context);
                });

            }
        }
    }
}
