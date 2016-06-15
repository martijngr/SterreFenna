using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SterreFenna.WebPresentation.Menus
{
    public class MenuModel
    {
        public MenuModel()
        {
            MenuItems = new List<MenuItem>();
        }

        public List<MenuItem> MenuItems { get; set; }
    }

    public class MenuItem
    {
        public MenuItem()
        {
            MenuItems = Enumerable.Empty<MenuItem>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string UniqueName { get; set; }
        public int Rank { get; set; }

        public IEnumerable<MenuItem> MenuItems { get; set; }
    }
}