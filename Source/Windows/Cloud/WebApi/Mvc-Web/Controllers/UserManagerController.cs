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
        public ActionResult Permissions()
        {
            return View();
        }
    }
}