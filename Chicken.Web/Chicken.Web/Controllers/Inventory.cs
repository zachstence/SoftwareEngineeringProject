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

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventory.Entities.Inventory item = db.Inventory.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Cost,Quantity")] Inventory.Entities.Inventory item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(item);
        }

      


    }
}