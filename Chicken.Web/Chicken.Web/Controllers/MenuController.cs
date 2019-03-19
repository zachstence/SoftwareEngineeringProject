using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chicken.Web.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        public ActionResult Index()
        {
            return Content("This is the Menu");
        }

        // For Testing
        public ActionResult Plates()
        {
            return Content("This are the plates");
        }

        // For Testing
        public ActionResult Sides()
        {
            return Content("This are the sides");
        }
    }
}