using System.Threading.Tasks;
using System.Collections.Generic;

namespace Anatoli.IdentityServer.Classes.PushNotifications
{
    public interface IPushNotificationProvider
    {
        Task<string> RegisterAppToken(string userId, string platform, string clientId, string appToken);
        Task DeactiveAppToken(string userId, string platform, string clientId, string appToken);
        Task CreateChannel(string channelName, List<string> userIds);
        Task RemoveChannel(string channelName);
        Task SubscribeChannel(List<string> channelNames, List<string> userIds);
        Task UnsubscribeChannel(string channelName, string userId);
        Task SendNotification(string message, List<string> userIds, string clientId, string platform);
        Task Broadcast(string channelName, string message, string clientId, string platform);
    }
}