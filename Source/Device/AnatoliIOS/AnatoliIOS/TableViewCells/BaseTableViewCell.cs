using System;
using UIKit;

namespace AnatoliIOS.TableViewCells
{
	public abstract class BaseTableViewCell : UITableViewCell
	{
		public BaseTableViewCell ()
		{
		}
		public BaseTableViewCell (IntPtr handle) : base (handle)
		{

		}
		public event EventHandler ItemRemoved;
		protected void OnItemRemoved(){
			if(ItemRemoved != null)
				ItemRemoved.Invoke(this,new EventArgs());
		}
	}
}

