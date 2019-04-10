using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Inventory.Entities;
using Chicken.Web.DataContexts;

namespace Chicken.Web.Controllers
{
    [Authorize]
    public class InventoryController : Controller
    {
        private InventoryDb db = new InventoryDb();
        // GET
        public ActionResult Index()
        { 
            return View(db.Inventory.ToList());
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public ActionResult Create([Bind(Include = "Id,Name,Cost,Quantity")] Inventory.Entities.Inventory item)
        {           
                if (ModelState.IsValid)
                {
                    db.Inventory.Add(item);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(item);        
        }
    }
}