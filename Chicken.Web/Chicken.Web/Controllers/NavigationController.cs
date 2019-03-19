using Chicken.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chicken.Web.Controllers
{
    public class NavigationController : Controller
    {
        // GET: Navigation
        // Creates the top Navigation bar
        public ActionResult TopNav()
        {
            var nav = new Navbar();
            return PartialView("_topNav", nav.NavbarTop());
        }
    }
}