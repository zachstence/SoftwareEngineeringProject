using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Inventory.Entities;

namespace Chicken.Web.DataContexts
{
    public class InventoryDb : DbContext
    {
        public InventoryDb() :
            base("DefaultConnection")
        {

        }

        public DbSet<Inventory.Entities.Inventory> Inventory { get; set; }
    }
}