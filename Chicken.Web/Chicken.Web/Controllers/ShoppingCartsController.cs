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


namespace Chicken.Web.Controllers
{
    public class ShoppingCartsController : Controller

    {

        public string ShoppingCartId { get; set; }

        private InventoryDb _db = new InventoryDb();
        private ShoppingCartDb sdb = new ShoppingCartDb();
        private IdentityDb idb = new IdentityDb();

        public const string CartSessionKey = "CartId";

        public ActionResult Index()
        {
            return View(sdb.ShoppingCarts.ToList());
        }

       
        // Called in the inventory view takes an item id as a parameter 
        public ActionResult OrderNow(int id)
        {
             
            // Gets the current users Id. 
            var UserIdD = User.Identity.GetUserId();

            
            // This makes a new Entry in the Shopping cart Database Assinging the UserId in the table to whoever clicked it
            // And the InventoryID of the item that was clicked. 
            // This will allow us to see everything that the one particular user ordered. in the next line.  
               var chicken = new ShoppingCart.Entities.ShoppingCart
                {
                    UserId = UserIdD,
                    InventoryID = id
      
                };

                // Playing with this idea to get the names of the items to display in the model... not quite sure yet how to do that. 
               
         
               // This goes through the shopping cart table and find all the entries that are equal to the current user's string id. 
               // if they are found they are turned into a list of Orders. and able to be passed into the View and we can see the User Id and 
               // The Inventory Item number

                   var UserOrders = from items in sdb.ShoppingCarts
                       where items.UserId.Contains(UserIdD)
                       select items;

                   var Orders = UserOrders.ToList();


                   // Again plaing withy this to see if I can get the Inventory name to show. 
                   foreach (var item in Orders)
                   {
                    var InventoryNames = from balls in _db.Inventory
                    where balls.Id == item.InventoryID
                    select balls;
                    var Names = InventoryNames.ToList();
                   }

            
                // This is necessary to actually add the created chicken(Shopping Cart database update) To the shopping cart. 
                // SaveChanges(); makes it stay. 
                sdb.ShoppingCarts.Add(chicken);
                sdb.SaveChanges();

                // This will return the index view when done, and I'm passing in the list of User Orders to tthe view so we can see just the user orders. 
                return View("Index", UserOrders);
           
            ;
             

           
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
            if (HttpContext.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(HttpContext.User.Identity.GetUserId()))
                {
                    HttpContext.Session[CartSessionKey] = HttpContext.User.Identity.GetUserId();
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




 //public string ShoppingCartId { get; set; }

    //private InventoryDb db = new InventoryDb();

    //private ShoppingCartDb sdb = new ShoppingCartDb();
    //private string strCart = "Cart";
  

    //public ActionResult OrderNow(int? id)
    //{

    //    ShoppingCartId = GetCartId();
    //    if (id == null)
    //    {
    //        return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
    //    }


    //    var item = db.Inventory.Find(id);
    //    var cartItem = db.ShoppingCartItems.SingleOrDefault(
    //        c => c.InventoryItem == ShoppingCartId
    //             && c.ProductId == id);


    //    if (Session[strCart] == null)
    //    {
    //        List<CartItem> lsCart = new List<CartItem>();
    //        {

    //            new CartItem(db.Inventory.Find(id), 1);
    //        }

    //        Session[strCart] = lsCart;
    //    }
    //    else
    //    {
    //        List<CartItem> lsCart = (List<CartItem>) Session[strCart];
    //        int check = isExistingCheck(id);
    //        if (check == -1)
    //            lsCart.Add(new CartItem(db.Inventory.Find(id), 1));
    //        else
    //            lsCart[check].Quantity++;
    //        Session[strCart] = lsCart;
    //    }

    //    return View("Index");

    //}


    //private int isExistingCheck(int? id)
    //{
    //    List<CartItem> lsCart = (List<CartItem>) Session[strCart];
    //    for (int i = 0; i < lsCart.Count; i++)
    //    {
    //        if (lsCart[i].InventoryItem.Id == id) return i;
    //    }

    //    return -1;
    //}

    //public string GetCartId()
    //{
    //    if (HttpContext.Session[strCart] == null)
    //    {
    //        if (!string.IsNullOrWhiteSpace(HttpContext.User.Identity.Name))
    //        {
    //            HttpContext.Session[strCart] = HttpContext.User.Identity.Name;
    //        }
    //        else
    //        {
    //            // Generate a new random GUID using System.Guid class.     
    //            Guid tempCartId = Guid.NewGuid();
    //            HttpContext.Session[strCart] = tempCartId.ToString();
    //        }
    //    }
    //    return HttpContext.Session[strCart].ToString();
    //}