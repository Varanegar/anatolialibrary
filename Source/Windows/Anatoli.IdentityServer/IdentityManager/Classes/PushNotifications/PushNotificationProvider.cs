using LinqKit;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Anatoli.IdentityServer.Entities;
using Anatoli.IdentityServer.Classes.Helpers;

namespace Anatoli.IdentityServer.Classes.PushNotifications
{
    public class PushNotificationProvider : IPushNotificationProvider
    {
        public enum Platforms
        {
            IOS,
            Android
        }

        #region Properties
        public Context DbContext
        {
            get;
            set;
        }
        #endregion

        #region Ctors
        public PushNotificationProvider() : this(new Context())
        {
        }

        public PushNotificationProvider(Context context)
        {
            DbContext = context;
        }
        #endregion

        #region Methods
        public async Task Broadcast(string channelName, string message, string clientId = "", string platform = "")
        {
            try
            {
                var userIds = DbContext.Channels
                                       .Where(p => p.Name == channelName)
                                       .SelectMany(s => s.Users.Select(ss => ss.Id))
                                       .ToList();

                await SendNotification(message, userIds, clientId, platform);
            }
            catch (Exception ex)
            {
                Serilog.Log.Logger.Error("error in SendNotification, {0}", ex);
            }
        }

        public async Task CreateChannel(string channelName, List<string> userIds = null)
        {
            try
            {
                var users = DbContext.Users.AsExpandable().InRange(p => p.Id, 1000, userIds).ToList();

                DbContext.Channels.Add(new Channel { Name = channelName, Users = users });

                await DbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Serilog.Log.Logger.Error("error in CreateChannel, {0}", ex);
            }
        }

        public async Task DeactiveAppToken(string userId, string platform, string clientId, string appToken)
        {
            try
            {
                var userDeviceToken = DbContext.UserDeviceToken
                                               .FirstOrDefault(p => p.UserId == userId &&
                                                                    p.Platform == platform &&
                                                                    p.Client.ClientId == clientId &&
                                                                    p.AppToken == appToken);
                userDeviceToken.IsActive = false;

                await DbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Serilog.Log.Logger.Error("error in DeactiveAppToken, {0}", ex);
            }
        }

        public async Task<string> RegisterAppToken(string userId, string platform, string clientId, string appToken)
        {
            try
            {
                var client = DbContext.Clients.FirstOrDefault(p => p.ClientId == clientId);

                if (!DbContext.UserDeviceToken.Any(p => p.AppToken == appToken && p.UserId == userId))
                    DbContext.UserDeviceToken.Add(new UserDeviceToken
                    {
                        Id = Guid.NewGuid(),
                        UserId = userId,
                        Platform = platform,
                        ClientId = client.Id,
                        AppToken = appToken,
                        IsActive = true
                    });

                await DbContext.SaveChangesAsync();

                return appToken;
            }
            catch (Exception ex)
            {
                Serilog.Log.Logger.Error("error in RegisterAppToken, {0}", ex);
            }

            return string.Empty;
        }

        public async Task RemoveChannel(string channelName)
        {
            try
            {
                var channel = DbContext.Channels.FirstOrDefault(p => p.Name == channelName);

                DbContext.Channels.Remove(channel);

                await DbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Serilog.Log.Logger.Error("error in RemoveChannel, {0}", ex);
            }
        }

        public async Task SendNotification(string message, List<string> userIds, string clientId = "", string platform = "")
        {
            try
            {
                var allSelectedUsersTokens = DbContext.UserDeviceToken
                                                      .AsExpandable()
                                                      .Where(p => p.Client.ClientId == clientId || clientId == string.Empty &&
                                                                  p.Platform == platform || platform == string.Empty)
                                                      .InRange(p => p.UserId, 1000, userIds)
                                                      .Select(s => new
                                                      {
                                                          s.AppToken,
                                                          s.Platform
                                                      }).ToList();

                //var allAppleUsers = allSelectedUsersTokens.Where(p => p.Platform == Platforms.IOS.ToString())
                //                                          .Select(s => s.AppToken)
                //                                          .ToList();

                //if (allAppleUsers.Count > 0)
                //    await Task.Factory.StartNew(() =>
                //    {
                //        new ApnsManager((notification, ex) =>
                //        {
                //            Serilog.Log.Logger.Information("notification process failed, {0}, {1}", notification, ex);
                //        }, (notification) =>
                //         {
                //             Serilog.Log.Logger.Information("notification process succeeded, {0}, {1}", notification);
                //         }).Send(message, allAppleUsers);
                //    });

                var allAndroidUsers = allSelectedUsersTokens.Where(p => p.Platform == Platforms.Android.ToString())
                                                            .Select(s => s.AppToken)
                                                            .ToList();
                if (allAndroidUsers.Count > 0)
                    await Task.Factory.StartNew(() =>
                    {
                        new GcmManager((notification, ex) =>
                        {
                            Serilog.Log.Logger.Information("notification process failed, {0}, {1}", notification, ex);
                        }, (notification) =>
                        {
                            Serilog.Log.Logger.Information("notification process succeeded, {0}, {1}", notification);
                        }).Send(message, allAndroidUsers);
                    });
            }
            catch (Exception ex)
            {
                Serilog.Log.Logger.Error("error in SendNotification, {0}", ex);
            }
        }

        public async Task SubscribeChannel(List<string> channelNames, List<string> userIds)
        {
            try
            {
                var users = DbContext.Users
                                     .AsExpandable()
                                     .InRange(p => p.Id, 1000, userIds)
                                     .ToList();

                channelNames.ForEach(itm =>
                {
                    var channel = DbContext.Channels.FirstOrDefault(p => p.Name == itm);
                    channel.Users = users;
                });

                await DbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Serilog.Log.Logger.Error("error in SubscribeChannel, {0}", ex);
            }
        }

        public async Task UnsubscribeChannel(string channelName, string userId)
        {
            try
            {
                var channel = DbContext.Channels.FirstOrDefault(p => p.Name == channelName);

                var user = DbContext.Users.FirstOrDefault(p => p.Id == userId);

                channel.Users.Remove(user);

                await DbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Serilog.Log.Logger.Error("error in UnsubscribeChannel, {0}", ex);
            }
        }
        #endregion
    }
}