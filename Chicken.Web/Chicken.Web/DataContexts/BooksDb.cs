using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Books.Entities;

namespace Chicken.Web.DataContexts
{
    public class BooksDb : DbContext
    {
        public BooksDb() :
            base("DefaultConnection")
        {
                
        }
        public DbSet<Book> Books { get; set; }
    }
}