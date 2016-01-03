using Anatoli.PMC.DataAccess.Helpers;
using Anatoli.PMC.DataAccess.Helpers.Entity;
using Anatoli.PMC.ViewModels.EVC;
using Anatoli.PMC.ViewModels.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thunderstruck;

namespace Anatoli.PMC.DataAccess.DataAdapter
{
    public class EVCAdapter : BaseAdapter
    {
        private static EVCAdapter instance = null;
        public static EVCAdapter Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EVCAdapter();
                }
                return instance;
            }
        }
        EVCAdapter() { }
        public  PMCEvcViewModel CalcEvcResult(PMCEvcViewModel data)
        {
            var resultEvc = new PMCEvcViewModel();
            var connectionString = StoreConfigHeler.Instance.GetStoreConfig(data.CenterId).ConnectionString;
            using(var context = new DataContext(data.CenterId.ToString(), connectionString, Transaction.No))
            {
                DateTime serverTime = GeneralCommands.GetServerDateTime(context);
                data.DateOf = new PersianDate(serverTime) + serverTime.ToString(" HH:mm");
                
                var evcId =
                data.EVCId = GeneralCommands.GetId(context, "Evc");

                DataObject<PMCEvcViewModel> evcDataObject = new DataObject<PMCEvcViewModel>("Evc", "InvalidId");
                evcDataObject.Insert(data, context);
                DataObject<PMCEvcDetailViewModel> evcDetailDataObject = new DataObject<PMCEvcDetailViewModel>("EvcDetail", "InvalidId");
                data.EVCDetail.ForEach(item =>
                    {
                        item.EvcDetailId = GeneralCommands.GetId(context, "Evc");
                        item.EVCID = data.EVCId;
                        evcDetailDataObject.Insert(item, context);
                    });
                context.Execute("EXEC ApplyPrice " + evcId);
                context.Execute("EXEC ApplyPromotion " + evcId);
                context.Execute("EXEC ApplyTaxCharge " + evcId);
                context.Execute("EXEC ApplyStock " + evcId);


                resultEvc = evcDataObject.Select.With(context).First("where EvcId='" + evcId + "'");
                var resultEvcDetail = evcDetailDataObject.Select.With(context).All("where EvcId='" + evcId + "'");
                resultEvc.EVCDetail = new List<PMCEvcDetailViewModel>();
                resultEvc.EVCDetail.AddRange(resultEvcDetail);
            }
            return resultEvc;
        }
    }
}
