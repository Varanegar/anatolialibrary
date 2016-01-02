using Anatoli.PMC.DataAccess.Helpers;
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
        private static SellAdapter instance = null;
        public static SellAdapter Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SellAdapter();
                }
                return instance;
            }
        }
        SellAdapter() { }
        public  void SavePurchaseOrder(PMCSellViewModel orderInfo)
        {
            var connectionString = StoreConfigHeler.Instance.GetStoreConfig(orderInfo.CenterId).ConnectionString;

            using (var context = new DataContext(connectionString, Transaction.Begin))
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
