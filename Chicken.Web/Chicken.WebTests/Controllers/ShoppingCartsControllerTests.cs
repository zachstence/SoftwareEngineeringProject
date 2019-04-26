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
using System.Data.Common;

namespace Chicken.Web.Controllers.Tests
{
    [TestClass()]
    public class ShoppingCartsControllerTests
    {

        static ShoppingCartsController controller;
       
        static InventoryDb db;
        static CartItem cartItem;
        static Inventory.Entities.Inventory invItem;

        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext) {
            controller = new ShoppingCartsController();
            TestUtil.SetFakeControllerContext(controller);

            // Create test ivnentory and cart items
            invItem = new Inventory.Entities.Inventory
            {
                Id = 2,
                Name = "Test Item",
                Cost = 1,
                Quantity = 1
            };

            cartItem = new CartItem
            {
                ItemId = "TestItemId",
                ProductId = 2,
                CartId = "UnitTest",
                Product = invItem,
                Quantity = 1,
                DateCreated = DateTime.Now
            };

            
        }

        [TestMethod()]
        public void ReduceQuantityTest()
        {

            var cartId = cartItem.CartId;
            var productId = cartItem.Product.Id;
            var before = cartItem.Product.Quantity;

            try
            {
                controller.ReduceQuantity(cartId, productId);
                var after = cartItem.Product.Quantity;
            }
            catch (Exception e)
            {
                Assert.Fail("Expected no exception but got" + e.Message);
            }
                
        }

        [TestMethod()]
        public void IndexTest() {
            var result = controller.Index() as ViewResult;
            Assert.AreEqual("Index", result.ViewName);
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

        [TestMethod()]
        public void RemoveItemTest()
        {
            var cartId = cartItem.CartId;
            var productId = cartItem.Product.Id;

            try
            {
                controller.RemoveItem(cartId, productId);
            }
            catch (Exception e)
            {
                Assert.Fail("Expected no exception but got" + e.Message);
            }
        }

        [TestMethod()]
        public void IncreaseQuantityTest()
        {
            var cartId = cartItem.CartId;
            var productId = cartItem.Product.Id;
            var before = cartItem.Product.Quantity;

            try
            {
                controller.IncreaseQuantity(cartId, productId);
                var after = cartItem.Product.Quantity;
            }
            catch (Exception e)
            {
                Assert.Fail("Expected no exception but got" + e.Message);
            }

        }
    }
}