using Parse;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Anatoli.Business.Parse
{
    public class ParseNotificationProvider : IPushNotificationProvider
    {
        #region Ctors
        public ParseNotificationProvider()
        {
            //ParseClient.Initialize("APPLICATION ID", ".NET KEY");
            ParseClient.Initialize("wUAgTsRuLdin0EvsBhPniG40O24i2nEGVFl8R5OI", "G7guVuyx35bb4fBOwo7BVhlG2L2E2qKLQI0sLAe0");
        }
        #endregion

        #region Methods
        public async Task SignupAsync(IdentityUser user)
        {
            try
            {
                var parseUser = new ParseUser()
                {
                    Username = user.UserName,
                    Password = user.PasswordHash,
                    Email = user.Email
                };

                // other fields can be set just like with ParseObject
                parseUser["phone"] = user.PhoneNumber;

                await parseUser.SignUpAsync();
            }
            catch (Exception ex)
            {
                //Todo: log4net
                throw;
            }
        }

        public async Task LoginAsync(IdentityUser user)
        {
            try
            {
                await ParseUser.LogInAsync(user.UserName, user.PasswordHash);
            }
            catch (Exception ex)
            {
                //Todo: log4net
                throw;
            }
        }

        public async Task CreateChannel(string channelName, List<IdentityUser> users)
        {
            try
            {
                var installations = await GetInstallations(users);

                foreach (var installation in installations)
                {
                    installation.AddUniqueToList("channels", channelName);

                    await installation.SaveAsync();
                }
            }
            catch (Exception ex)
            {
                //Todo: log4net
                throw;
            }
        }
        public async Task Subscribe(List<string> channelNames, List<IdentityUser> users)
        {
            try
            {
                var installations = await GetInstallations(users);

                foreach (var installation in installations)
                {
                    installation.AddRangeToList("channels", channelNames);

                    await installation.SaveAsync();
                }
            }
            catch (Exception ex)
            {
                //Todo: log4net
                throw;
            }
        }
        private async Task<List<ParseInstallation>> GetInstallations(List<IdentityUser> users)
        {
            var userNames = users.Select(s => s.UserName);

            var userQuery = ParseUser.Query.Where(p => userNames.Contains(p.Username));

            var query = from installation in ParseInstallation.Query
                        join user in userQuery on installation["user"] equals user
                        select installation;

            var installations = await query.FindAsync();

            return installations.ToList();
        }

        public async Task UnSubscribe(string channelName, IdentityUser user)
        {
            try
            {
                var installation = GetInstallation(user).Result;

                installation.RemoveAllFromList("channels", new List<string> { channelName });

                await installation.SaveAsync();
            }
            catch (Exception ex)
            {
                //Todo: log4net
                throw;
            }
        }
        private async Task<ParseInstallation> GetInstallation(IdentityUser user)
        {
            var userQuery = ParseUser.Query.Where(p => p.Username == user.UserName);

            var query = from installation in ParseInstallation.Query
                        join _user in userQuery on installation["user"] equals _user
                        select installation;

            return await query.FirstAsync();
        }

        public async Task SendNotification(string message, List<IdentityUser> users, ParseGeoPoint? location = null)
        {
            try
            {
                var userNames = users.Select(s => s.UserName);

                var userQuery = ParseUser.Query.Where(p => userNames.Contains(p.Username));

                if (location.HasValue)
                    userQuery = userQuery.WhereWithinDistance("location", location.Value, ParseGeoDistance.FromMiles(1));

                var push = new ParsePush()
                {
                    Query = from installation in ParseInstallation.Query
                            join user in userQuery on installation["user"] equals user
                            select installation,
                    Alert = message
                };

                await push.SendAsync();
            }
            catch (Exception ex)
            {
                //Todo: log4net
                throw;
            }
        }

        public async Task SendData(IDictionary<string, object> data, List<IdentityUser> users, ParseGeoPoint? location = null)
        {
            try
            {
                var userNames = users.Select(s => s.UserName);

                var userQuery = ParseUser.Query.Where(p => userNames.Contains(p.Username));

                if (location.HasValue)
                    userQuery = userQuery.WhereWithinDistance("location", location.Value, ParseGeoDistance.FromMiles(1));

                var push = new ParsePush()
                {
                    Query = from installation in ParseInstallation.Query
                            join user in userQuery on installation["user"] equals user
                            select installation,
                    Data = data
                };

                await push.SendAsync();
            }
            catch (Exception ex)
            {
                //Todo: log4net
                throw;
            }
        }

        public async Task Broadcast(string channelName, string message, ParseGeoPoint? location = null)
        {
            try
            {
                var push = new ParsePush()
                {
                    Channels = new List<string> { channelName },
                    Alert = message
                };

                await push.SendAsync();
            }
            catch (Exception)
            {
                //Todo: log4net
                throw;
            }
        }

        public async Task Broadcast(string channelName, IDictionary<string, object> data, ParseGeoPoint? location = null)
        {
            try
            {
                var push = new ParsePush()
                {
                    Channels = new List<string> { channelName },
                    Data = data
                };

                await push.SendAsync();
            }
            catch (Exception ex)
            {
                //Todo: log4net
                throw;
            }
        }
        #endregion
    }
}
