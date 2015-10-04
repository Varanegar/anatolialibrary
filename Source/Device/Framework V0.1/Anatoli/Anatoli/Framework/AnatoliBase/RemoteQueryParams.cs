using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.Framework.AnatoliBase
{
    public class RemoteQueryParams
    {
        public Tuple<string, string>[] Parameters { get; set; }
        public string Endpoint { get; set; }
        public RemoteQueryParams(string endPoint, params Tuple<string, string>[] parameters)
        {
            Parameters = parameters;
            Endpoint = endPoint;
        }
    }
}
