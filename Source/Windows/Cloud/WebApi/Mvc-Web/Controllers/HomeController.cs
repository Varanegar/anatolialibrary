using System.Web.Mvc;

namespace Mvc_Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("ReviewProductRequest", "Products");
        }
    }
}
