using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;

namespace AnatoliIOS
{
    public class MenuItem
    {
        public string Title { get; set; }
        public UIImage Icon { get; set; }
		public MenuType Type{ get; set; }
		public enum MenuType{
			Login,
			FirstPage,
			Profile,
			Products,
			Favorits,
			Stores
		}
    }
}
