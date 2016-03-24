using Anatoli.Business.Parse;
using Anatoli.Cloud.WebApi.Infrastructure;
using Anatoli.Cloud.WebApi.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Anatoli.Cloud.WebApi.Controllers
{
    [Authorize(Roles="Admin")]
    [RoutePrefix("api/push/parse")]
    public class RolesController : BaseApiController
    {
        //[Route("sendmessagetouser")]
        //public async Task<IHttpActionResult> PushMessage(string ApplicationOwnerId, string appName, string username, string message)
        //{

        //    var pushProvider = new ParseNotificationProvider();
        //    //pushProvider.SendNotification("سلام" + username, )
        //    //return OK();

        //}
    }
}
