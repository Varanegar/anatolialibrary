using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;

namespace AnatoliIOS.Components
{
    public class MenuItem
    {
        public string Title { get; set; }
        public UIImage Icon { get; set; }
		public MenuType Type{ get; set; }
		public enum MenuType{
			Login,
			Profile,
			Products,
			Favorits
		}
    }
}
