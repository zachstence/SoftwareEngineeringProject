using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Chicken.Web.DataContexts;
using Chicken.Web.Models;

namespace Chicken.Web.Controllers
{
    public class OrderController : Controller
    {
        private InventoryDb db = new InventoryDb();

        // GET: Order
        public ActionResult Index(string sortOrder, string searchString)
        {
            // thrown into the view as categorys. 
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var shoppingCartItems = db.ShoppingCartItems.Include(c => c.Product);

            // search bar 
            if (!String.IsNullOrEmpty(searchString))
            {
                shoppingCartItems = shoppingCartItems.Where(s => s.CartId.Contains(searchString)
                                               || s.Product.Name.Contains(searchString) );
            }

            // How we want to switch.
            switch (sortOrder)
            {
                case "name_desc":
                    
                    shoppingCartItems = shoppingCartItems.OrderBy(s => s.Product.Name);
                    break;
                case "Date":
                    shoppingCartItems = shoppingCartItems.OrderBy(s => s.Quantity);
                    break;
                case "date_desc":
                    shoppingCartItems = shoppingCartItems.OrderBy(s => s.DateCreated);
                    break;
                default:
                    shoppingCartItems = shoppingCartItems.OrderBy(s => s.CartId);

                    break;
            }
            return View("Index", shoppingCartItems.ToList());
        }

        // GET: Order/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartItem cartItem = db.ShoppingCartItems.Find(id);
            if (cartItem == null)
            {
                return HttpNotFound();
            }
            return View("Details", cartItem);
        }

        // GET: Order/Create
        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(db.Inventory, "Id", "Name");
            return View();
        }

        // POST: Order/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ItemId,CartId,Quantity,DateCreated,ProductId")] CartItem cartItem)
        {
            if (ModelState.IsValid)
            {
                db.ShoppingCartItems.Add(cartItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductId = new SelectList(db.Inventory, "Id", "Name", cartItem.ProductId);
            return View("Create", cartItem);
        }

        // GET: Order/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartItem cartItem = db.ShoppingCartItems.Find(id);
            if (cartItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(db.Inventory, "Id", "Name", cartItem.ProductId);
            return View(cartItem);
        }

        // POST: Order/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemId,CartId,Quantity,DateCreated,ProductId")] CartItem cartItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cartItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(db.Inventory, "Id", "Name", cartItem.ProductId);
            return View(cartItem);
        }

        // GET: Order/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartItem cartItem = db.ShoppingCartItems.Find(id);
            if (cartItem == null)
            {
                return HttpNotFound();
            }
            return View(cartItem);
        }

        // POST: Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CartItem cartItem = db.ShoppingCartItems.Find(id);
            db.ShoppingCartItems.Remove(cartItem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
