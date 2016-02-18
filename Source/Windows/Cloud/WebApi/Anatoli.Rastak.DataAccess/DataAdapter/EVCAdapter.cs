using Anatoli.Rastak.DataAccess.Helpers;
using Anatoli.Rastak.DataAccess.Helpers.Entity;
using Anatoli.Rastak.ViewModels.EVC;
using Anatoli.Rastak.ViewModels.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thunderstruck;

namespace Anatoli.Rastak.DataAccess.DataAdapter
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
        public  RastakEvcViewModel CalcEvcResult(RastakEvcViewModel data)
        {
            var resultEvc = new RastakEvcViewModel();
            var connectionString = StoreConfigHeler.Instance.GetStoreConfig(data.CenterId).ConnectionString;
            using(var context = new DataContext(data.CenterId.ToString(), connectionString, Transaction.No))
            {
                try
                {
                    DateTime serverTime = GeneralCommands.GetServerDateTime(context);
                    data.DateOf = new PersianDate(serverTime) + serverTime.ToString(" HH:mm");

                    var evcId =
                    data.EVCId = GeneralCommands.GetId(context, "Evc");

                    DataObject<RastakEvcViewModel> evcDataObject = new DataObject<RastakEvcViewModel>("Evc", "InvalidId");
                    evcDataObject.Insert(data, context);
                    DataObject<RastakEvcDetailViewModel> evcDetailDataObject = new DataObject<RastakEvcDetailViewModel>("EvcDetail", "InvalidId");
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
                    resultEvc.EVCDetail = new List<RastakEvcDetailViewModel>();
                    resultEvc.EVCDetail.AddRange(resultEvcDetail);
                }
                catch (Exception ex)
                {
                    context.Rollback();

                    log.Error(ex.Message, ex);
                    throw ex;
                }
                finally
                {
                    context.Dispose();
                }
            }
            return resultEvc;
        }
    }
}
