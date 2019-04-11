using System.Data.Entity;
using Chicken.Web.Models;

namespace Chicken.Web.DataContexts
{
    public class ShoppingCartDb : DbContext
    {
        public ShoppingCartDb() :
            base("DefaultConnection")
        {

        }

        public DbSet<ShoppingCart.Entities.ShoppingCart> ShoppingCarts { get; set; }

        public System.Data.Entity.DbSet<Inventory.Entities.Inventory> Inventories { get; set; }
    }
}