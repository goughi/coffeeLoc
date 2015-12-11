using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace coffee.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Coffee Locator.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Having trouble? send us a message";

            return View();
        }
        [HttpPost]
        public ActionResult Contact(String message)
        {
            ViewBag.Message = "Thanks! We got your message";
            return View();

        }
    }
}