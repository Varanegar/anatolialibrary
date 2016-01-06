using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNAppServer.Common.CustomerSetting
{
    public class AnatoliWebServiceConfig : ConfigurationSection
    {
        public AnatoliWebServiceConfig() { }

        public AnatoliWebServiceConfig(string webServiceName, string uRI)
        {
            WebServiceName = webServiceName;
            URI = uRI;
        }

        [ConfigurationProperty("WebServiceName", DefaultValue = "", IsRequired = true, IsKey = true)]
        public string WebServiceName
        {
            get { return (string)this["WebServiceName"]; }
            set { this["WebServiceName"] = value; }
        }

        [ConfigurationProperty("URI", DefaultValue = "", IsRequired = true, IsKey = false)]
        public string URI
        {
            get { return (string)this["URI"]; }
            set { this["URI"] = value; }
        }
    }
}
