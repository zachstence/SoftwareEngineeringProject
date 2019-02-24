using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ClassLibrary1;

namespace ChickenFeed.DataContexts
{
    public class ChickenDb : DbContext
    {
        public DbSet<Book> Books { get; set; }
    }
}