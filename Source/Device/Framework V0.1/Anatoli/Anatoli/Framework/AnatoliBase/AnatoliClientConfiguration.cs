using RestSharp.Portable.Authenticators.OAuth2.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.Framework.AnatoliBase
{
    class AnatoliClientConfiguration : IClientConfiguration
    {
        public string ClientId
        {
            get { return "petropay"; }
        }

        public string ClientPublic
        {
            get { return "petropay"; }
        }

        public string ClientSecret
        {
            get { return "petropay"; }
        }

        public string ClientTypeName
        {
            get { return "bearer"; }
        }

        public bool IsEnabled
        {
            get { return true; }
        }

        public string RedirectUri
        {
            get { return Configuration.WebService.PortalAddress; }
        }

        public string Scope
        {
            get { return "foo bar"; }
        }
    }
}
