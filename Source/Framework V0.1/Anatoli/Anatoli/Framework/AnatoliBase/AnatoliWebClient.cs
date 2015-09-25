using RestSharp.Portable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.Anatoliclient
{
    public abstract class AnatoliWebClient
    {
        public abstract bool IsOnline();

        internal async Task<Result> SendPostRequestAsync<Result>(string requestUri, params Tuple<string, string>[] parameters)
        {
            var client = new RestClient(Configuration.PortalUri);
            var request = new RestRequest(requestUri, HttpMethod.Post);
            
            foreach (var item in parameters)
            {
                Parameter p = new Parameter();
                p.Name = item.Item1;
                p.Value = item.Item2;
                request.AddParameter(p);
            }
            var asyncHandle = await client.Execute<Result>(request);
            return asyncHandle.Data;
        }
    }
    public class RequestResult
    {
        public string Content { get; set; }
    }
    public class AnatoliMetaInfo
    {
        public string ErrorString { get; set; }
        public string ErrorCode { get; set; }
        public bool Result { get; set; }
    }
}
