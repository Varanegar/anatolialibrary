using System.Web.Mvc;

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