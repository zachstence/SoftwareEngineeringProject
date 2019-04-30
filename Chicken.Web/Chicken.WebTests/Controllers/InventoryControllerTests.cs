using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chicken.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chicken.WebTests;
using System.Web.Mvc;
using System.Net;
using Chicken.Web.Models;
using Chicken.Web.DataContexts;

namespace Chicken.Web.Controllers.Tests
{
    [TestClass()]
    public class InventoryControllerTests
    {

        static InventoryController controller;
        static InventoryDb db;
        static CartItem cartItem;
        static Inventory.Entities.Inventory invItem;

        [TestInitialize()]
        public void TestInitialize()
        {

            db = new InventoryDb();

            invItem = new Inventory.Entities.Inventory
            {
                Id = 1,
                Name = "Test Item",
                Cost = 1,
                Quantity = 1
            };

            cartItem = new CartItem
            {
                ItemId = "TestItemId",
                ProductId = 1,
                CartId = "UnitTest",
                Product = invItem,
                Quantity = 1,
                DateCreated = DateTime.Now
            };

            var ci = db.ShoppingCartItems.SingleOrDefault(
                c => c.CartId == "UnitTest"
                 && c.ProductId == 1);
            ci.Quantity = 1;

            db.Inventory.Add(invItem);
            //db.ShoppingCartItems.Add(cartItem);

            controller = new InventoryController(db);
            TestUtil.SetFakeControllerContext(controller);

        }

        [TestMethod()]
        public void IndexTest()
        {
            var result = controller.Index() as ActionResult;
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var resultAsView = result as ViewResult;
            Assert.AreEqual("Index", resultAsView.ViewName);
        }

        [TestMethod()]
        public void CreateTest()
        {
            var result = controller.Create() as ActionResult;
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var resultAsView = result as ViewResult;
            Assert.AreEqual("Create", resultAsView.ViewName);
        }

        [TestMethod()]
        public void AddToCartTest()
        {
            int id = 1;

            int beforeQuantity = cartItem.Quantity;

            var result = controller.AddToCart(id) as ActionResult;
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));

            var resultAsredirectToRouteResult = result as RedirectToRouteResult;
            var dict = resultAsredirectToRouteResult.RouteValues;

            object actionName;
            dict.TryGetValue("action", out actionName);
            Assert.AreEqual("Index", actionName);

            object quantity;
            dict.TryGetValue("Quantity", out quantity);

            Assert.AreEqual(beforeQuantity + 1, quantity);
        }

        /*
        [TestMethod()]
        public void EditTest()
        {
            Inventory.Entities.Inventory invItem = new Inventory.Entities.Inventory();
            var result = controller.Edit(invItem) as ActionResult;
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var resultAsView = result as ViewResult;
            Assert.AreEqual("Edit", resultAsView.ViewName);
            var model = resultAsView.Model;
            Assert.IsInstanceOfType(model, typeof(Inventory.Entities.Inventory));
        }
        */

        [TestMethod()]
        public void DeleteNullTest()
        {
            var result = controller.Delete(null);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
            var code = result as HttpStatusCodeResult;
            Assert.AreEqual((int) HttpStatusCode.BadRequest, code.StatusCode);
        }

        /*
        [TestMethod()]
        public void DeleteTest()
        {

        }
        */

        /*
        [TestMethod()]
        public void DeleteConfirmedTest()
        {
            int id = 2;
            var result = controller.DeleteConfirmed(id) as ActionResult;
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var resultAsView = result as ViewResult;
            Assert.AreEqual("Index", resultAsView.ViewName);
        }
        */

        [TestMethod()]
        public void GetCartIdTest()
        {
            var result = controller.GetCartId();
            Assert.AreEqual("UnitTest", result);
        }

        [TestMethod()]
        public void GetCartItemsTest()
        {
            var result = controller.GetCartItems();
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<CartItem>));
        }
    }
}