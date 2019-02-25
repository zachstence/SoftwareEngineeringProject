using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Chicken.Web.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Chicken.Web.DataContexts
{
    public class IdentityDb : IdentityDbContext<ApplicationUser>
    {
        public IdentityDb()
            : base("DefaultConnection")
        {
        }


    }
}