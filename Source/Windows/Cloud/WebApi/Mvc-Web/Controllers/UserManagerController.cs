using System.Web.Mvc;

namespace Mvc_Web.Controllers
{
    public class UserManagerController : Controller
    {
        // GET: UserManager
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Edit(string id)
        {
            ViewBag.userId = id;

            return View();
        }

        public ActionResult Permissions()
        {
            return View();
        }

        public ActionResult Stocks()
        {
            return View();
        }

        public ActionResult ChangePassword()
        {
            return View();
        }
    }
}