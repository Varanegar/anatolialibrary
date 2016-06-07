using PushSharp.Apple;
using PushSharp.Core;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;

namespace Anatoli.IdentityServer.Classes.PushNotifications
{
    public class ApnsManager
    {
        public ApnsConfiguration Config { get; set; }
        public ApnsServiceBroker Broker { get; set; }
        //"\\Certificate_for_push_develop.p12"
        //pushcert.p12
        const string ApnsCertificateFile = "\\vn_cert_v1.p12";
        const string ApnsCertificatePassword = "123456";
        public ApnsManager(NotificationFailureDelegate<ApnsNotification> onNotificationFailed, NotificationSuccessDelegate<ApnsNotification> onNotificationSucceeded)
        {

            var path = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
            var directory = Path.GetDirectoryName(path);

            var apnsCertificateFile = directory.Replace("file:\\", "") + ApnsCertificateFile;

            Config = new ApnsConfiguration(ApnsConfiguration.ApnsServerEnvironment.Sandbox, apnsCertificateFile, ApnsCertificatePassword);
            Broker = new ApnsServiceBroker(Config);
            Broker.OnNotificationFailed += onNotificationFailed;
            Broker.OnNotificationSucceeded += onNotificationSucceeded;
        }

        public void Send(string msg, List<string> apnsDeviceTokens)
        {
            Broker.Start();

            foreach (var dt in apnsDeviceTokens)
                Broker.QueueNotification(new ApnsNotification
                {
                    DeviceToken = dt,
                    Payload = JObject.Parse("{\"aps\":{\"badge\":7}}")
                });

            Broker.Stop();
        }

        public void APNS_Feedback_Service(FeedbackService.FeedbackReceivedDelegate onFeedbackReceived)
        {
            var config = new ApnsConfiguration(ApnsConfiguration.ApnsServerEnvironment.Sandbox, ApnsCertificateFile, ApnsCertificatePassword);
            var fbs = new FeedbackService(config);

            fbs.FeedbackReceived += onFeedbackReceived;

            //fbs.FeedbackReceived += (string deviceToken, DateTime timestamp) =>
            //{
            //    // Remove the deviceToken from your database
            //    // timestamp is the time the token was reported as expired
            //};

            fbs.Check();
        }
    }
}