﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Inventory.Entities;
using Chicken.Web.DataContexts;
using Chicken.Web.Models;

namespace Chicken.Web.Controllers
{
    [Authorize]
    public class InventoryController : Controller
    {
        private InventoryDb db = new InventoryDb();

        public string ShoppingCartId { get; set; }

        private InventoryDb _db = new InventoryDb();

        public const string CartSessionKey = "CartId";
        // GET
        public ActionResult Index(string searchString)
        {
          
            var inventoryItems = db.Inventory.OrderBy(x => x.Category).ToList();

            // search bar 
            if (!String.IsNullOrEmpty(searchString))
            {
                inventoryItems = db.Inventory.Where(s =>  s.Name.Contains(searchString) || s.Category.Contains(searchString)).OrderBy(x =>x.Name).ToList() ;
            }

            return View("Index", inventoryItems);
        }

        public ActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public ActionResult Create([Bind(Include = "Id,Name,Cost,Quantity,Category")] Inventory.Entities.Inventory item)
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
        public ActionResult Edit([Bind(Include = "Id,Name,Cost,Quantity,Category")] Inventory.Entities.Inventory item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Edit", item);
        }

        // GET: Books/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Inventory.Entities.Inventory item = db.Inventory.Find(id);
            db.Inventory.Remove(item);
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


        public ActionResult AddToCart(int id)
        {
            // Retrieve the product from the database.           
            ShoppingCartId = GetCartId();

            var cartItem = _db.ShoppingCartItems.SingleOrDefault(
                c => c.CartId == ShoppingCartId
                     && c.ProductId == id);
            if (cartItem == null)
            {
                // Create a new cart item if no cart item exists.                 
                cartItem = new CartItem
                {
                    ItemId = Guid.NewGuid().ToString(),
                    ProductId = id,
                    CartId = ShoppingCartId,
                    Product = _db.Inventory.SingleOrDefault(
                        p => p.Id == id),
                    Quantity = 1,
                    DateCreated = DateTime.Now
                };

                _db.ShoppingCartItems.Add(cartItem);
                cartItem.Product.Quantity--;

            }
            else
            {
                // If the item does exist in the cart,                  
                // then add one to the quantity.                 
                cartItem.Quantity++;
                cartItem.Product.Quantity--;
            }
            _db.SaveChanges();

            var Chicken = GetCartItems();

            return RedirectToAction("Index");
        }

        public string GetCartId()
        {
            if (HttpContext.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(HttpContext.User.Identity.Name))
                {
                    HttpContext.Session[CartSessionKey] = HttpContext.User.Identity.Name;
                }
                else
                {
                    // Generate a new random GUID using System.Guid class.     
                    Guid tempCartId = Guid.NewGuid();
                    HttpContext.Session[CartSessionKey] = tempCartId.ToString();
                }
            }
            return HttpContext.Session[CartSessionKey].ToString();
        }

        public List<CartItem> GetCartItems()
        {
            ShoppingCartId = GetCartId();

            return _db.ShoppingCartItems.Where(
                c => c.CartId == ShoppingCartId).ToList();
        }




    }
}