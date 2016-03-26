using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Thinktecture.IdentityModel.Client;

namespace Anatoli.Business.Helpers
{
    public class InterServerCommunication
    {
        private static readonly log4net.ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private string InternalUsername = "AnatoliMobileApp";
        private string InternalPassword = "Anatoli@App@Vn";

        private TimeSpan TokenExpireTimeSpan = new TimeSpan();
        private string ApplicationOwnerId { get; set; }

        private static InterServerCommunication instance = null;
        private TokenResponse OAuthResult;
        private InterServerCommunication(){
            ApplicationOwnerId = "";
        }
        public static InterServerCommunication Instance
        {
            get
            {
                if (instance == null)
                    instance = new InterServerCommunication();
                return instance;
            }
        }

        public string GetInternalServerToken(string applicationOwnerId)
        {
            try
            {
                if ((ApplicationOwnerId == null || ApplicationOwnerId == "") && (applicationOwnerId == null || applicationOwnerId == ""))
                {
                    log.Fatal("Invalid owner in internal communication");
                    throw new Exception("Invalid App Owner");
                }
                else if (applicationOwnerId != null && applicationOwnerId != "" && applicationOwnerId != ApplicationOwnerId)
                {
                    ApplicationOwnerId = applicationOwnerId;
                    OAuthResult = null;
                }

                if (OAuthResult == null)
                {
                    var client = new HttpClient();
                    var oauthClient = new OAuth2Client(new Uri(ConfigurationManager.AppSettings["InternalServer"] + "/oauth/token"));

                    client.Timeout = TimeSpan.FromMinutes(2);
                    OAuthResult = oauthClient.RequestResourceOwnerPasswordAsync(InternalUsername, InternalPassword, ApplicationOwnerId).Result;
                    {
                        if (OAuthResult.AccessToken == null || OAuthResult.AccessToken == "")
                        {
                            OAuthResult = null;
                            log.Fatal("Can not login into internal servers");
                            throw new Exception("Internal communication failed");
                        }
                    }
                }

                return OAuthResult.AccessToken;
            }
            catch (Exception ex)
            {
                log.Error("Can not login to internal server", ex);
                throw ex;
            }
        }



    }
}
