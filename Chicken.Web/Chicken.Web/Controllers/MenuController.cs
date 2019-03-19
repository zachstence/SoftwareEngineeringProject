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

        // Gets the Starters items
        public ActionResult Starters()
        {
            return Content("This are the plates");
        }

        // Gets the Individual Pieces items
        public ActionResult IndividualPieces()
        {
            return Content("This are the plates");
        }

        // Gets the Sides items
        public ActionResult Sides()
        {
            return Content("This are the sides");
        }

        // Gets the Plates items
        public ActionResult Plates()
        {
            return Content("This are the plates");
        }

        // Gets the Snacks items
        public ActionResult Snacks()
        {
            return Content("This are the plates");
        }

        // Gets the Specials items
        public ActionResult Specials()
        {
            return Content("This are the plates");
        }

        // Gets the Kid's Meal items
        public ActionResult KidsMeals()
        {
            return Content("This are the plates");
        }

        // Gets the Bevarage items
        public ActionResult Beverages()
        {
            return Content("This are the plates");
        }
    }
}