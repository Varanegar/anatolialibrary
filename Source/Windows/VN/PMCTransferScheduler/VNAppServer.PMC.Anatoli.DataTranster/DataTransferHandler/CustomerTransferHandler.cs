using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Anatoli.PMC.DataAccess.DataAdapter;
using VNAppServer.Anatoli.PMC.Helpers;
using VNAppServer.Anatoli.Common;
using Anatoli.ViewModels.User;

namespace VNAppServer.PMC.Anatoli.DataTranster
{
    public class CustomerTransferHandler
    {
        private static readonly string CustomerDataType = "Customer";
        private static readonly log4net.ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static void UploadCustomerToServer(HttpClient client, string serverURI, string privateOwnerQueryString)
        {
            try
            {
                log.Info("Start CallServerService URI ");
                var currentTime = DateTime.Now;
                var lastUpload = Utility.GetLastUploadTime(CustomerDataType);
                var dbData = CustomerAdapter.Instance.GetNewUsers(lastUpload);
                if (dbData != null)
                {
                    dbData.ForEach(item =>
                        {
                            item.PrivateOwnerId = Guid.Parse(privateOwnerQueryString);
                            string data = JsonConvert.SerializeObject(item);
                            string URI = serverURI + UriInfo.SaveUserURI + privateOwnerQueryString;
                            UserReturnModel result = ConnectionHelper.CallServerServicePost<UserReturnModel>(data, URI, client);
                            CustomerAdapter.Instance.SetCustomerSiteUserId(result.Id, result.Mobile);

                        });
                }
                Utility.SetLastUploadTime(CustomerDataType, currentTime);

                log.Info("Completed CallServerService ");
            }
            catch(Exception ex)
            {
                log.Error("Failed CallServerService ", ex);
            }
        }
    }
}
