using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chicken.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;
using Moq;
using Chicken.WebTests;

namespace Chicken.Web.Controllers.Tests
{
    [TestClass()]
    public class ShoppingCartsControllerTests
    {

        static ShoppingCartsController controller;

        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext) {
            controller = new ShoppingCartsController();
            TestUtil.SetFakeControllerContext(controller);
        }

        [TestMethod()]
        public void ReduceQuantityTest()
        {

        }

        [TestMethod()]
        public void IndexTest() {
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
            var result = controller.GetCartId();
            Assert.AreEqual("UnitTest", result);
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