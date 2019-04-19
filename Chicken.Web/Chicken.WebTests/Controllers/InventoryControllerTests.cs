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
            var result = controller.Index() as ActionResult;
            Console.WriteLine(result.ToString());
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
            Assert.Fail();
        }

        [TestMethod()]
        public void EditTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteConfirmedTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void AddToCartTest1()
        {
            Assert.Fail();
        }

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