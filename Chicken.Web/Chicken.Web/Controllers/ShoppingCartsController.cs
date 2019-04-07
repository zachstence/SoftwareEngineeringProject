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
using ShoppingCart.Entities;

namespace Chicken.Web.Controllers
{
    public class ShoppingCartsController : Controller
    {
        private InventoryDb db = new InventoryDb();
        private ShoppingCartDb sdb = new ShoppingCartDb();
        private string strCart = "Cart";
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult OrderNow(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            if (Session[strCart] == null)
            {
                List<CartItem> lsCart = new List<CartItem>();
                {
                   
                    new CartItem(db.Inventory.Find(id), 1);
                }

                Session[strCart] = lsCart;
            }
            else
            {
                List<CartItem> lsCart = (List<CartItem>) Session[strCart];
                int check = isExistingCheck(id);
                if (check == -1)
                    lsCart.Add(new CartItem(db.Inventory.Find(id), 1));
                else
                    lsCart[check].Quantity++;
                Session[strCart] = lsCart;
            }

            return View("Index");

        }


        private int isExistingCheck(int? id)
        {
            List<CartItem> lsCart = (List<CartItem>) Session[strCart];
            for (int i = 0; i < lsCart.Count; i++)
            {
                if (lsCart[i].InventoryItem.Id == id) return i;
            }

            return -1;
        }
      

    }
}

