using PushSharp.Apple;
using PushSharp.Core;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Anatoli.IdentityServer.Classes.PushNotifications
{
    public class ApnsManager
    {
        public ApnsConfiguration Config { get; set; }
        public ApnsServiceBroker Broker { get; set; }

        const string ApnsCertificateFile = "asd";
        const string ApnsCertificatePassword = "asd";
        public ApnsManager(NotificationFailureDelegate<ApnsNotification> onNotificationFailed, NotificationSuccessDelegate<ApnsNotification> onNotificationSucceeded)
        {
            Config = new ApnsConfiguration(ApnsConfiguration.ApnsServerEnvironment.Sandbox, ApnsCertificateFile, ApnsCertificatePassword);
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
                    Payload = JObject.Parse("{ \"aps\" : { \"alert\" : " + msg + " } }")
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