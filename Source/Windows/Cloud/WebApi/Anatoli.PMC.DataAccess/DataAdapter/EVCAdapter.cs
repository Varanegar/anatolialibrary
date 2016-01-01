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
        public static PMCEvcViewModel CalcEvcResult(PMCEvcViewModel data)
        {
            var resultEvc = new PMCEvcViewModel();
            using(var context = new DataContext(Transaction.Begin))
            {
                DateTime serverTime = GeneralCommands.GetServerDateTime(context);
                data.DateOf = new PersianDate(serverTime) + serverTime.ToString(" HH:mm");
                
                var evcId = context.Execute<PMCGetIdEntity>("EXEC GetId 'Evc' , @EvcId output").Id;
                data.EVCId = evcId;

                DataObject<PMCEvcViewModel> evcDataObject = new DataObject<PMCEvcViewModel>("Evc");
                evcDataObject.Insert(data, context);
                DataObject<PMCEvcDetailViewModel> evcDetailDataObject = new DataObject<PMCEvcDetailViewModel>("EvcDetail");
                data.EVCDetail.ForEach(item =>
                    {
                        evcDetailDataObject.Insert(item, context);
                    });
                context.Execute("EXEC ApplyPrice " + evcId);
                context.Execute("EXEC ApplyPromotion " + evcId);
                context.Execute("EXEC ApplyTaxCharge " + evcId);
                context.Execute("EXEC ApplyStock " + evcId);


                resultEvc = evcDataObject.Select.First("where EvcId='" + evcId + "'");
                var resultEvcDetail = evcDetailDataObject.Select.All("where EvcId='" + evcId + "'");
                resultEvc.EVCDetail.AddRange(resultEvcDetail);
                context.Commit();
            }
            return resultEvc;
        }
    }
}
