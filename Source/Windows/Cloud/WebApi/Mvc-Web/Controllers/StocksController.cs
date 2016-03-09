using System;
using System.Web.Mvc;

namespace Mvc_Web.Controllers
{
    public class StocksController : Controller
    {
        // GET: Stock
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductRequestRules()
        {
            return View();
        }

        public ActionResult ProductRequestRule(string id)
        {
            ViewBag.ruleId = id;

            ViewBag.pToday = PersianDate.ConvertDate.ToFa(DateTime.Now);
            ViewBag.pNextMonth = PersianDate.ConvertDate.ToFa(DateTime.Now.AddMonths(1));

            return View();
        }

        public ActionResult Products() {
            return View();
        }
    }
}