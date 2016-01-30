using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;
using Parse;

namespace Anatoli.Business.Parse
{
    public interface IPushNotificationProvider
    {
        Task SignupAsync(IdentityUser user);
        Task LoginAsync(IdentityUser user);
        Task CreateChannel(string channelName, List<IdentityUser> users);
        Task Subscribe(List<string> channelNames, List<IdentityUser> users);
        Task UnSubscribe(string channelName, IdentityUser user);
        Task SendNotification(string message, List<string> users, ParseGeoPoint? location = null);
        Task SendData(IDictionary<string, object> data, List<IdentityUser> users, ParseGeoPoint? location = null);
        Task Broadcast(string channelName, string message, ParseGeoPoint? location = null);
        Task Broadcast(string channelName, IDictionary<string, object> data, ParseGeoPoint? location = null);
    }
}