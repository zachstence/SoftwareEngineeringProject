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
    /// <summary>
    /// Class responsible for testing the Inventory class.
    /// </summary>
    public class InventoryControllerTests
    {

        static InventoryController controller;
        static InventoryDb db;
        static CartItem cartItem;
        static Inventory.Entities.Inventory invItem;

        [TestInitialize()]
        /// <summary>
        /// Runs before each test to initialize all data structures to use in testing. Creates a mock inventory and cart item and adds them to the
        /// database. Then sets the mock database and context in the controller to use for testing.
        /// </summary>
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
        /// <summary>
        /// Tests to make sure the Index method returns the proper view.
        /// </summary>
        public void IndexTest()
        {
            var result = controller.Index() as ActionResult;
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var resultAsView = result as ViewResult;
            Assert.AreEqual("Index", resultAsView.ViewName);
        }

        [TestMethod()]
        /// <summary>
        /// Tests to make sure the Create method returns the proper view.
        /// </summary>
        public void CreateTest()
        {
            var result = controller.Create() as ActionResult;
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var resultAsView = result as ViewResult;
            Assert.AreEqual("Create", resultAsView.ViewName);
        }

        [TestMethod()]
        /// <summary>
        /// Tests to make sure the AddToCart method properly adds an item to the cart and the product quantities in the cart and inventory
        /// database reflect the changes.
        /// </summary>
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

        [TestMethod()]
        /// <summary>
        /// Tests to make sure the Edit method returns the proper view.
        /// </summary>
        public void EditTest()
        {
            int id = 1;
            var result = controller.Edit(id) as ActionResult;
            Assert.IsInstanceOfType(result, typeof(ViewResult));

            var resultAsView = result as ViewResult;
            Assert.AreEqual("Edit", resultAsView.ViewName);

            var model = resultAsView.Model;
            Assert.IsInstanceOfType(model, typeof(Inventory.Entities.Inventory));

        }

        [TestMethod()]
        /// <summary>
        /// Tests to make sure the Edit method returns the proper view when passed a null id.
        /// </summary>
        public void EditNullTest()
        {
            int? id = null;
            var result = controller.Edit(id);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
            var code = result as HttpStatusCodeResult;
            Assert.AreEqual((int)HttpStatusCode.BadRequest, code.StatusCode);
        }

        [TestMethod()]
        /// <summary>
        /// Tests to make sure the Edit method returns the proper view when passed an id that is not found in the database.
        /// </summary>
        public void EditNotFoundTest()
        {
            int id = -1;
            var result = controller.Edit(id) as ActionResult;
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }

        [TestMethod()]
        /// <summary>
        /// Tests to make sure the Delete method returns the proper view when passed a null id.
        /// </summary>
        public void DeleteNullTest()
        {
            var result = controller.Delete(null);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
            var code = result as HttpStatusCodeResult;
            Assert.AreEqual((int) HttpStatusCode.BadRequest, code.StatusCode);
        }

        [TestMethod()]
        /// <summary>
        /// Tests to make sure the Delete method returns the proper view.
        /// </summary>
        public void DeleteTest()
        {
            int id = 1;
            var result = controller.Delete(id) as ActionResult;
            Assert.IsInstanceOfType(result, typeof(ViewResult));

            var resultAsView = result as ViewResult;
            Assert.AreEqual("Delete", resultAsView.ViewName);

            var model = resultAsView.Model;
            Assert.IsInstanceOfType(model, typeof(Inventory.Entities.Inventory));
        }

        [TestMethod()]
        /// <summary>
        /// Tests to make sure the DeleteConfirmed method returns the proper view.
        /// </summary>
        public void DeleteConfirmedTest()
        {
            int id = 1;
            var result = controller.DeleteConfirmed(id) as ActionResult;
            var resultAsredirectToRouteResult = result as RedirectToRouteResult;
            var dict = resultAsredirectToRouteResult.RouteValues;

            object actionName;
            dict.TryGetValue("action", out actionName);
            Assert.AreEqual("Index", actionName);
        }

        [TestMethod()]
        /// <summary>
        /// Tests to make sure the GetCartId method returns the proper cart id from the mocked controller context.
        /// </summary>
        public void GetCartIdTest()
        {
            var result = controller.GetCartId();
            Assert.AreEqual("UnitTest", result);
        }

        [TestMethod()]
        /// <summary>
        /// Tests to make sure the GetCartItems method returns the proper view and model.
        /// </summary>
        public void GetCartItemsTest()
        {
            var result = controller.GetCartItems();
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<CartItem>));
        }
    }
}