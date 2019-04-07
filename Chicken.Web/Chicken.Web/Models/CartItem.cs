using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;

namespace Chicken.Web.Models
{
    public class CartItem
    {
    
       public Inventory.Entities.Inventory InventoryItem { get; set; }
       public int Quantity { get; set; }

       public CartItem(Inventory.Entities.Inventory inventoryItem, int quantity)
       {
           InventoryItem = inventoryItem;
           Quantity = quantity;
       }

    }
}