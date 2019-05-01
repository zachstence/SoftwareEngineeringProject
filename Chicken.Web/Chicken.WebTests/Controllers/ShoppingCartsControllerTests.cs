﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    /// <summary>
    /// Class responsible for testing the ShoppingCartsController class.
    /// </summary>
    public class ShoppingCartsControllerTests
    {

        static ShoppingCartsController controller;
        static InventoryDb db;
        static CartItem cartItem;
        static Inventory.Entities.Inventory invItem;

        [TestInitialize()]
        /// <summary>
        /// Runs before each test to initialize all data structures to use in testing. Creates a mock inventory and cart item and adds them to the
        /// database. Then sets the mock database and context in the controller to use for testing.
        /// </summary>
        public void TestInitalize() {

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

            db.Inventory.Add(invItem);

            var ci = db.ShoppingCartItems.SingleOrDefault(
                c => c.CartId == "UnitTest"
                && c.ProductId == 1);
            ci.Quantity = 1;

            //db.ShoppingCartItems.Add(cartItem);

            controller = new ShoppingCartsController(db);
            TestUtil.SetFakeControllerContext(controller);

        }

        [TestMethod()]
        /// <summary>
        /// Tests the ReduceQuantity method. Uses the mock data structures to make sure reducing the quantity of an item causes the correct changes
        /// in quantity in the database.
        /// </summary>
        public void ReduceQuantityTest()
        {

            var cartId = cartItem.CartId;
            var productId = cartItem.Product.Id;

            var beforeQuantity = cartItem.Product.Quantity;
            var result = controller.ReduceQuantity(cartId, productId);

            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));

            var resultAsredirectToRouteResult = result as RedirectToRouteResult;
            var dict = resultAsredirectToRouteResult.RouteValues;

            object actionName;
            dict.TryGetValue("action", out actionName);
            Assert.AreEqual("Index", actionName);

            object quantity;
            dict.TryGetValue("Quantity", out quantity);
            Assert.AreEqual(beforeQuantity - 1, quantity);

        }

        [TestMethod()]
        /// <summary>
        /// Tests to make sure the Index method returns the proper view.
        /// </summary>
        public void IndexTest() {
            var result = controller.Index() as ViewResult;
            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod()]
        /// <summary>
        /// Tests to make sure the controller returns the correct cart ID set in the mock HttpContext.
        /// </summary>
        public void GetCartIdTest()
        {
            var result = controller.GetCartId();
            Assert.AreEqual("UnitTest", result);
        }

        [TestMethod()]
        /// <summary>
        /// Tests to make sure that the controller return the cart items present in the database.
        /// </summary>
        public void GetCartItemsTest()
        {
            var result = controller.GetCartItems();
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<CartItem>));
        }


        [TestMethod()]
        /// <summary>
        /// Tests the IncreaseQuantity method. Uses the mock data structures to make sure increasing the quantity of an item causes the correct changes
        /// in quantity in the database.
        /// </summary>
        public void IncreaseQuantityTest()
        {
            var cartId = cartItem.CartId;
            var productId = cartItem.Product.Id;

            var beforeQuantity = cartItem.Product.Quantity;
            var result = controller.IncreaseQuantity(cartId, productId);

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

    }
}