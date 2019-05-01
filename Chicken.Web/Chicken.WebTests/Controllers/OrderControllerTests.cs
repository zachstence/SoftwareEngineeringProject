using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chicken.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chicken.WebTests;
using System.Web.Mvc;
using Chicken.Web.Models;
using System.Net;
using Chicken.Web.DataContexts;

namespace Chicken.Web.Controllers.Tests
{
    [TestClass()]
    /// <summary>
    /// Class responsible for testing the OrderController class.
    /// </summary>
    public class OrderControllerTests
    {

        static OrderController controller;
        static InventoryDb db;
        static CartItem cartItem;
        static Inventory.Entities.Inventory invItem;

        [TestInitialize()]
        /// <summary>
        /// Runs before each test to initialize the controller to use in testing. Sets the mock context in the controller to use for testing.
        /// </summary>
        public void TestInitialize()
        {
            db = new InventoryDb();

            invItem = new Inventory.Entities.Inventory
            {
                Id = 2,
                Name = "Test Item2",
                Cost = 1,
                Quantity = 1
            };

            cartItem = new CartItem
            {
                ItemId = "TestItemId2",
                ProductId = 2,
                CartId = "UnitTest2",
                Product = invItem,
                Quantity = 1,
                DateCreated = DateTime.Now
            };

            db.Inventory.Add(invItem);
            db.ShoppingCartItems.Add(cartItem);

            var ci = db.ShoppingCartItems.SingleOrDefault(
                c => c.CartId == "UnitTest"
                 && c.ProductId == 2);
            ci.Quantity = 1;

            controller = new OrderController(db);
            TestUtil.SetFakeControllerContext(controller);
        }

        [TestMethod()]
        /// <summary>
        /// Tests to make sure the Index method returns the proper view.
        /// </summary>
        public void IndexTest()
        {
            var result = controller.Index("", "") as ActionResult;
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var resultAsView = result as ViewResult;
            Assert.AreEqual("Index", resultAsView.ViewName);
        }

        [TestMethod()]
        /// <summary>
        /// Tests to make sure the Details method returns the proper view when the id passed is not found in the database.
        /// </summary>
        public void DetailsNotFoundTest()
        {
            // Should get HttpNotFoundResult with a bad item id
            var result = controller.Details("") as ActionResult;
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }

        [TestMethod()]
        /// <summary>
        /// Tests to make sure the Edit method returns the proper view when passed a null id.
        /// </summary>
        public void EditNullTest()
        {
            int? id = null;
            var result = controller.Edit(id.ToString());
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
            var code = result as HttpStatusCodeResult;
            Assert.AreEqual((int)HttpStatusCode.NotFound, code.StatusCode);
        }

        [TestMethod()]
        /// <summary>
        /// Tests to make sure the Edit method returns the proper view when passed an id that is not found in the database.
        /// </summary>
        public void EditNotFoundTest()
        {
            int id = -1;
            var result = controller.Edit(id.ToString()) as ActionResult;
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
            Assert.AreEqual((int)HttpStatusCode.BadRequest, code.StatusCode);
        }

        [TestMethod()]
        [ExpectedException(typeof(System.ArgumentNullException))]
        /// <summary>
        /// Tests to make sure the DeleteConfirmed method throws the proper exception when the id passed is not found in the database.
        /// </summary>
        public void DeleteConfirmedNotFoundTest()
        {
            var result = controller.DeleteConfirmed("") as ActionResult;
        }

    }
}