using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chicken.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using FakeHttpContext;
using System.Web;
using Moq;

namespace Chicken.Web.Controllers.Tests
{
    [TestClass()]
    public class ShoppingCartsControllerTests
    {
        [TestMethod()]
        public void ReduceQuantityTest()
        {
            var invController = new InventoryController();
            var cartController = new ShoppingCartsController();
            invController.AddToCart(1);
            var items = cartController.GetCartItems();
            Console.WriteLine(items);
        }

        [TestMethod()]
        public void IndexTest() {

            var controller = new ShoppingCartsController();
            var result = controller.Index() as ViewResult;
            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod()]
        public void DisposeTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetCartIdTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetCartItemsTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void RemoveItemTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ReduceQuantityTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void IncreaseQuantityTest()
        {
            Assert.Fail();
        }
    }
}