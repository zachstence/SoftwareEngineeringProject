using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Chicken.Web.DataContexts;
using Chicken.Web.Models;
using Microsoft.AspNet.Identity;
using ShoppingCart.Entities;
using Moq;


namespace Chicken.Web.Controllers
{
    [Authorize]
    public class ShoppingCartsController : Controller

   
    {

        public string ShoppingCartId { get; set; }

        private InventoryDb _db = new InventoryDb();

        public const string CartSessionKey = "CartId";

        public ActionResult Index()
        {
            return View(GetCartItems());
        }



        public void Dispose()
        {
            if (_db != null)
            {
                _db.Dispose();
                _db = null;
            }
        }

        public string GetCartId()
        {

            Console.WriteLine(HttpContext == null);
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

        public ActionResult RemoveItem(string removeCartID, int removeProductID)
        {
           // using (var _db = new WingtipToys.Models.ProductContext())

           using(var _db = new InventoryDb())
            {
                try
                {
                    var myItem = (from item in _db.ShoppingCartItems where item.CartId == removeCartID && item.Product.Id == removeProductID select item).FirstOrDefault();
                    if (myItem != null)
                    {
                        // Remove Item.
                        myItem.Product.Quantity = myItem.Product.Quantity + myItem.Quantity;
                        _db.ShoppingCartItems.Remove(myItem);
                        _db.SaveChanges();
                    }
                }
                catch (Exception exp)
                {
                    throw new Exception("ERROR: Unable to Remove Cart Item - " + exp.Message.ToString(), exp);
                }
            }

           return RedirectToAction("Index");
        }


        public ActionResult ReduceQuantity(string removeCartID, int removeProductID)
        {
            // using (var _db = new WingtipToys.Models.ProductContext())

            using (var _db = new InventoryDb())
            {
                try
                {
                    var myItem = (from item in _db.ShoppingCartItems where item.CartId == removeCartID && item.Product.Id == removeProductID select item).FirstOrDefault();
                    if (myItem != null)
                    {
                        // Remove Item.
                        myItem.Quantity--;
                        myItem.Product.Quantity++;
                        
                        _db.SaveChanges();
                    }
                }
                catch (Exception exp)
                {
                    throw new Exception("ERROR: Unable to Remove Cart Item - " + exp.Message.ToString(), exp);
                }
            }

            return RedirectToAction("Index");
        }

        public ActionResult IncreaseQuantity(string removeCartID, int removeProductID)
        {
          

            using (var _db = new InventoryDb())
            {
                try
                {
                    var myItem = (from item in _db.ShoppingCartItems where item.CartId == removeCartID && item.Product.Id == removeProductID select item).FirstOrDefault();
                    if (myItem != null)
                    {
                        // Remove Item.
                        myItem.Quantity++;
                        myItem.Product.Quantity--;
                        _db.SaveChanges();
                    }
                }
                catch (Exception exp)
                {
                    throw new Exception("ERROR: Unable to Remove Cart Item - " + exp.Message.ToString(), exp);
                }
            }

            return RedirectToAction("Index");
        }

    }
   



}

