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
using Anatoli.ViewModels.BaseModels;
using VNAppServer.Anatoli.Common;

namespace VNAppServer.PMC.Anatoli.DataTranster
{
    public class StorePictureTransferHandler
    {
        private static readonly string PictureDataType = "StorePicture";
        private static readonly log4net.ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static void UploadStorePictureToServer(HttpClient client, string serverURI, string privateOwnerId, string dataOwner, string dataOwnerCenter)
        {
            try
            {
                log.Info("Start CallServerService URI ");
                var currentTime = DateTime.Now;
                var lastUpload = Utility.GetLastUploadTime(PictureDataType);

                List<ItemImageViewModel> dataList = ImageAdapter.Instance.CenterPictures(lastUpload);

                string URI = serverURI + UriInfo.SaveImageURI;
                ConnectionHelper.CallServerService(dataList, client, URI, privateOwnerId, dataOwner, dataOwnerCenter);
                Utility.SetLastUploadTime(PictureDataType, currentTime);

                log.Info("Completed CallServerService ");
            }
            catch(Exception ex)
            {
                log.Error("Failed CallServerService ", ex);
            }
        }
    }
}
