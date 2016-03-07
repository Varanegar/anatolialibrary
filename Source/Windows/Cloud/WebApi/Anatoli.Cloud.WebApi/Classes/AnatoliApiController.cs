using System;
using System.Web;
using System.Collections.Generic;
using Anatoli.ViewModels.StockModels;
using Anatoli.Cloud.WebApi.Controllers;
using Anatoli.ViewModels.ProductModels;

namespace Anatoli.Cloud.WebApi.Classes
{
    public abstract class AnatoliApiController : BaseApiController
    {


        public Guid OwnerKey
        {
            get
            {
                return Guid.Parse(HttpContext.Current.Request.Headers["OwnerKey"]);
            }
        }
    }
}