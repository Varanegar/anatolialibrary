using RestSharp.Portable.Authenticators.OAuth2;
using RestSharp.Portable.Authenticators.OAuth2.Configuration;
using RestSharp.Portable.Authenticators.OAuth2.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.Framework.AnatoliBase
{
    class AnatoliOAuthClient : OAuth2Client
    {
        public AnatoliOAuthClient(IRequestFactory factory, IClientConfiguration configuration)
            : base(factory, configuration)
        {
        }
        protected override Endpoint AccessCodeServiceEndpoint
        {
            get
            {
                return new Endpoint
                {
                    BaseUri = AnatoliBase.Configuration.WebService.PortalAddress,
                    Resource = AnatoliBase.Configuration.WebService.OAuthTokenUrl
                };
            }
        }

        protected override Endpoint AccessTokenServiceEndpoint
        {
            get
            {
                return new Endpoint
                {
                    BaseUri = AnatoliBase.Configuration.WebService.PortalAddress,
                    Resource = AnatoliBase.Configuration.WebService.OAuthTokenUrl
                };
            }
        }

        public override string Name
        {
            get { return "Anatoli"; }
        }

        protected override RestSharp.Portable.Authenticators.OAuth2.Models.UserInfo ParseUserInfo(string content)
        {
            throw new NotImplementedException();
        }

        protected override Endpoint UserInfoServiceEndpoint
        {
            get
            {
                return new Endpoint
                {
                    BaseUri = AnatoliBase.Configuration.WebService.PortalAddress,
                    Resource = AnatoliBase.Configuration.WebService.OAuthTokenUrl
                };
            }
        }
    }
}
