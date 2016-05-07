using System.Web.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Anatoli.IdentityServer.Classes.Helpers;
using Anatoli.IdentityServer.Classes.PushNotifications;

namespace Anatoli.IdentityServer.Controllers
{
    [Authorize]
    [RoutePrefix("api/notification")]
    public class NotificationController : ApiController
    {
        public class RequestModel
        {
            public string userId { get; set; }
            public string channelName { get; set; }
            public string platform { get; set; }
            public string message { get; set; }
            public List<string> userIds { get; set; }
            public string clientId { get; set; }
            public string appToken { get; set; }
            public List<string> channelNames { get; internal set; }
        }

        [HttpGet, Route("test")]
        public IHttpActionResult Test()
        {
            var userId = User.GetClaimUserId();

            return Ok(userId);
        }

        [HttpPost,Route("registerToken")]
        public async Task<IHttpActionResult> RegisterAppToken([FromBody] RequestModel data)
        {
            await new PushNotificationProvider().RegisterAppToken(data.userId, data.platform, data.clientId, data.appToken);

            return Ok();
        }

        [HttpPost, Route("unsubscribeChannel")]
        public async Task<IHttpActionResult> UnsubscribeChannel([FromBody] RequestModel data)
        {
            await new PushNotificationProvider().UnsubscribeChannel(data.channelName, data.userId);

            return Ok();
        }

        [Authorize(Roles = "Admin"), Route("createChannel"), HttpPost]
        public async Task<IHttpActionResult> CreateChannel([FromBody] RequestModel data)
        {
            await new PushNotificationProvider().CreateChannel(data.channelName, data.userIds);

            return Ok();
        }

        [Authorize(Roles = "Admin"), Route("sendNotification"), HttpPost]
        public async Task<IHttpActionResult> SendNotification([FromBody] RequestModel data)
        {
            await new PushNotificationProvider().SendNotification(data.message, data.userIds, data.clientId, data.platform);

            return Ok();
        }

        [Authorize(Roles = "Admin"), Route("broadcast"), HttpPost]
        public async Task<IHttpActionResult> Broadcast([FromBody] RequestModel data)
        {
            await new PushNotificationProvider().Broadcast(data.channelName, data.message, data.clientId, data.platform);

            return Ok();
        }

        [Authorize(Roles = "Admin"), Route("subscribeChannel"), HttpPost]
        public async Task<IHttpActionResult> SubscribeChannel([FromBody] RequestModel data)
        {
            await new PushNotificationProvider().SubscribeChannel(data.channelNames, data.userIds);

            return Ok();
        }
    }
}
