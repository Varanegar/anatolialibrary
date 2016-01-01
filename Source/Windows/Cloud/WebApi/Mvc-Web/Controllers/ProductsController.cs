using System;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;

namespace Mvc_Web.Controllers
{
    public class ProductsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ReviewProductRequest()
        {
            return View();
        }
        
        public ActionResult StoreRequestsHistory()
        {
            return View();        
        }
    }
}