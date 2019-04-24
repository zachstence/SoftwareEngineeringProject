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

namespace Chicken.Web.Controllers.Tests
{
    [TestClass()]
    public class InventoryControllerTests
    {

        static InventoryController controller;

        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            controller = new InventoryController();
            TestUtil.SetFakeControllerContext(controller);
        }

        [TestMethod()]
        public void IndexTest()
        {
            var result = controller.Index("Dark") as ActionResult;
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

        /*
        [TestMethod()]
        public void AddToCartTest()
        {
            int id = 2;
            var result = controller.AddToCart(id) as ActionResult;
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var resultAsView = result as ViewResult;
            Assert.AreEqual("Index", resultAsView.ViewName);
            var items = controller.GetCartItems();
            Console.WriteLine(items);
        }
        */

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