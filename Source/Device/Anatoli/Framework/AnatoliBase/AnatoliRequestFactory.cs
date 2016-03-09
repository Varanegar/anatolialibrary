using RestSharp.Portable;
using RestSharp.Portable.Authenticators.OAuth2.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.Framework.AnatoliBase
{
    class AnatoliRequestFactory : IRequestFactory
    {
        public RestSharp.Portable.IRestClient CreateClient()
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(Configuration.WebService.PortalAddress);
            return client;
        }

        public RestSharp.Portable.IRestRequest CreateRequest(string resource)
        {
            var request = new RestRequest(resource);
            request.AddHeader("Accept", "application/json");
            request.Method = System.Net.Http.HttpMethod.Post;
            return request;
        }
    }
}
