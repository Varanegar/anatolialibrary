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
using Anatoli.ViewModels;

namespace VNAppServer.PMC.Anatoli.DataTranster
{
    public class GeneralDataTransferHandler<T>
        where T : BaseViewModel, new()
    {
        private static readonly log4net.ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static void UploadDataToServer(HttpClient client, string serverURI, string privateOwnerQueryString)
        {
            try
            {
                log.Info("Start CallServerService URI ");
                var currentTime = DateTime.Now;
                var lastUpload = Utility.GetLastUploadTime(typeof(T).ToString().ToLower());
                var dbData = GeneralDBAdapter<T>.Instance.GetAllData(lastUpload);
                if (dbData != null)
                {
                    string data = JsonConvert.SerializeObject(dbData);
                    string URI = serverURI + UriInfo.GetSaveURI(typeof(T)) + privateOwnerQueryString;
                    var result = ConnectionHelper.CallServerServicePost(data, URI, client);
                }


                dbData = GeneralDBAdapter<T>.Instance.GetAllData(lastUpload);
                if (dbData != null)
                {
                    string data = JsonConvert.SerializeObject(dbData);
                    string URI = serverURI + UriInfo.GetCheckDeletedURI(typeof(T)) + privateOwnerQueryString;
                    var result = ConnectionHelper.CallServerServicePost(data, URI, client);

                }

                Utility.SetLastUploadTime(typeof(T).ToString().ToLower(), currentTime);

                log.Info("Completed CallServerService ");
            }
            catch(Exception ex)
            {
                log.Error("Failed CallServerService ", ex);
            }
        }
    }
}
