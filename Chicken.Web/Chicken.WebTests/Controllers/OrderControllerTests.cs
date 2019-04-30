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
    public class OrderControllerTests
    {

        private static OrderController controller;

        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            controller = new OrderController();
            //TestUtil.SetFakeControllerContext(controller);
        }

        [TestMethod()]
        public void IndexTest()
        {
            var result = controller.Index("", "") as ActionResult;
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var resultAsView = result as ViewResult;
            Assert.AreEqual("Index", resultAsView.ViewName);
        }

        [TestMethod()]
        public void DetailsNotFoundTest()
        {
            // Should get HttpNotFoundResult with a bad item id
            var result = controller.Details("") as ActionResult;
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }

        [TestMethod()]
        public void EditNotFoundTest()
        {
            // Should get HttpNotFoundResult with a bad item id
            var result = controller.Edit("") as ActionResult;
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }

        [TestMethod()]
        public void DeleteNotFoundTest()
        {
            // Should get HttpNotFoundResult with a bad item id
            var result = controller.Delete("") as ActionResult;
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }

        [TestMethod()]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void DeleteConfirmedNotFoundTest()
        {
            var result = controller.DeleteConfirmed("") as ActionResult;
        }

    }
}