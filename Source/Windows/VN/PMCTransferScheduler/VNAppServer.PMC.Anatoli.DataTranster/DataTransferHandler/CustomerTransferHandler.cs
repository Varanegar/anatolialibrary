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
        public static void UploadCustomerToServer(HttpClient client, string serverURI, string dataOwner, string dataOwnerCenter, string privateOwnerId)
        {
            try
            {
                log.Info("Start CallServerService URI ");
                var currentTime = DateTime.Now;
                var lastUpload = Utility.GetLastUploadTime(CustomerDataType);
                var dbData = CustomerAdapter.Instance.GetNewUsers(DateTime.MinValue);
                if (dbData != null)
                {
                    dbData.ForEach(item =>
                        {
                            try
                            {
                                item.SendPassSMS = false;
                                string data = JsonConvert.SerializeObject(item);
                                string getUserURI = serverURI + UriInfo.GetUserURI + "/" + item.Mobile;
                                var user = ConnectionHelper.CallServerServicePost<UserReturnModel>(data, getUserURI, client, privateOwnerId, dataOwner, dataOwnerCenter);
                                if (user != null)
                                    CustomerAdapter.Instance.SetCustomerSiteUserId(user.Id, user.Mobile);
                                else
                                {
                                    string URI = serverURI + UriInfo.SaveUserURI;
                                    UserReturnModel result = ConnectionHelper.CallServerServicePost<UserReturnModel>(data, URI, client, privateOwnerId, dataOwner, dataOwnerCenter);
                                    CustomerAdapter.Instance.SetCustomerSiteUserId(result.Id, result.Mobile);
                                }
                            }
                            catch (Exception ex)
                            {
                                log.Error("Failed CallServerService ", ex);
                            }

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
