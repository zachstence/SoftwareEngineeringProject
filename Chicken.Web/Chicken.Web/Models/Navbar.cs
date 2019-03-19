using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chicken.Web.Models
{
    public class Navbar
    {
        public IEnumerable<NavbarItem> NavbarTop()
        {
            var topNav = new List<NavbarItem>();
            topNav.Add(new NavbarItem() { Id = 1, action = "Index", nameOption = "Home", controller = "Home", isParent = false, parentId = -1 });
            topNav.Add(new NavbarItem() { Id = 2, action = "About", nameOption = "About", controller = "Home", isParent = false, parentId = -1 });
            // drop down Menu 
            topNav.Add(new NavbarItem() { Id = 3, action = "Index", nameOption = "Menu", controller = "Menu", isParent = true, parentId = -1 });
            topNav.Add(new NavbarItem() { Id = 4, action = "Plates", nameOption = "Plates", controller = "Menu", isParent = false, parentId = 3 });
            topNav.Add(new NavbarItem() { Id = 5, action = "Sides", nameOption = "Sides", controller = "Menu", isParent = false, parentId = 3 });
            // End drop down Menu
            topNav.Add(new NavbarItem() { Id = 7, action = "Contact", nameOption = "Contact", controller = "Home", isParent = false, parentId = -1 });
            return topNav;
        }
    }
}