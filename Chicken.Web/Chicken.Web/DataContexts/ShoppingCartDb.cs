using System.Data.Entity;

namespace Chicken.Web.DataContexts
{
    public class ShoppingCartDb : DbContext
    {
        public ShoppingCartDb() :
            base("DefaultConnection")
        {

        }

        public DbSet<ShoppingCart.Entities.ShoppingCart> ShoppingCarts { get; set; }
    }
}