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
using Chicken.Web.DataContexts;
using Chicken.Web.Models;
using Inventory;

namespace Chicken.Web.Controllers.Tests
{
    [TestClass()]
    public class ShoppingCartsControllerTests
    {

        static ShoppingCartsController controller;
        static InventoryDb db;
        static CartItem item;
        static Inventory.Entities.Inventory invItem;

        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext) {
            controller = new ShoppingCartsController();
            TestUtil.SetFakeControllerContext(controller);

            db = new InventoryDb();

            // Create test ivnentory and cart items
            invItem = new Inventory.Entities.Inventory
            {
                Id = 1,
                Name = "Test Item",
                Cost = 1,
                Quantity = 1
            };

            item = new CartItem
            {
                ItemId = Guid.NewGuid().ToString(),
                ProductId = 1,
                CartId = "UnitTest",
                Product = invItem,
                Quantity = 1,
                DateCreated = DateTime.Now
            };

            

        }

        [TestMethod()]
        public void ReduceQuantityTest()
        {
            var cartId = item.CartId;
            var productId = item.ProductId;

            controller.ReduceQuantity(cartId, productId);

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