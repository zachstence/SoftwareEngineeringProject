using System.Collections.Generic;
using Inventory.Entities;

namespace ShoppingCart.Entities
{
    public class ShoppingCart
    {
         public int Id { get; set; }
         public int InventoryID { get; set; }
         public string UserId { get; set; }
    }
}