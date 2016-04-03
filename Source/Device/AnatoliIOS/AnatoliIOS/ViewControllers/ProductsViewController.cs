using System;

using UIKit;
using AnatoliIOS.TableViewSources;
using Anatoli.App.Manager;
using AnatoliIOS.TableViewCells;
using Foundation;
using CoreGraphics;

namespace AnatoliIOS.ViewControllers
{
	public partial class ProductsViewController : BaseController
    {
		UISearchBar _searchBar;
		ProductsTableViewSource _productsTableViewSource;
        public ProductsViewController()
            : base("ProductsViewController", null)
        {
        }

        public async override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
			Title = "گروه بندی کالاها";
			var searchIcon = UIImage.FromFile("ic_search_white_24dp");
			var button = new UIButton(new CGRect(10f,10f,30f,30f));
			button.ContentMode = UIViewContentMode.ScaleAspectFill;
			button.TouchUpInside += (object sender, EventArgs e) => {
				_searchBar.Alpha = 0;
				UIView.Animate(0.5,0,UIViewAnimationOptions.TransitionFlipFromLeft ,()=>{
					productsTableView.TableHeaderView = _searchBar;
					_searchBar.Alpha = 1;
				},null);
			};
			var searchButton = new UIBarButtonItem (button);

			button.SetBackgroundImage (searchIcon, UIControlState.Normal);
			NavigationItem.RightBarButtonItem = searchButton;


			_searchBar = new UISearchBar ();
			_searchBar.Placeholder = "نام کالا یا گروه کالا را جستجو نمایید";
			_searchBar.SizeToFit ();
			_searchBar.AutocorrectionType = UITextAutocorrectionType.No;
			_searchBar.ShowsCancelButton = true;
			_searchBar.SearchButtonClicked += async (object sender, EventArgs e) => 	{
				_productsTableViewSource.SetDataQuery(ProductManager.Search(_searchBar.Text.Trim(),AnatoliApp.GetInstance().DefaultStore.store_id));
				await _productsTableViewSource.RefreshAsync();
				productsTableView.ReloadData();
			};
			_searchBar.CancelButtonClicked += (object sender, EventArgs e) => {
				
				UIView.Animate(0.5,0,UIViewAnimationOptions.TransitionFlipFromLeft ,
					()=>{
						_searchBar.Alpha = 0;
					},
					async ()=>{
						_searchBar.Text = "";
						productsTableView.TableHeaderView = null;
						_productsTableViewSource.SetDataQuery(ProductManager.GetAll(AnatoliApp.GetInstance().DefaultStore.store_id));
						await _productsTableViewSource.RefreshAsync();
						productsTableView.ReloadData();
					});
			};



			_productsTableViewSource = new ProductsTableViewSource();
            if (AnatoliApp.GetInstance().DefaultStore == null)
            {
                var store = await StoreManager.GetDefaultAsync();
                if (store != null)
                {
                    AnatoliApp.GetInstance().DefaultStore = store;
                }
                else
                {
                    AnatoliApp.GetInstance().PushViewController(new StoresViewController());
                    return;
                }
            }
			_productsTableViewSource.SetDataQuery(ProductManager.GetAll(AnatoliApp.GetInstance().DefaultStore.store_id));
			await _productsTableViewSource.RefreshAsync();
			_productsTableViewSource.Updated += (object sender, EventArgs e) => {
				productsTableView.ReloadData();
			};
			//productsTableView.RegisterNibForCellReuse(UINib.FromName(ProductSummaryViewCell.Key, NSBundle.MainBundle), ProductSummaryViewCell.Key);
			productsTableView.Source = _productsTableViewSource;
            productsTableView.ReloadData();
        }

		[Export("searchBarSearchButtonClicked:")]
		public virtual void SearchButtonClicked(UISearchBar searchBar){
			searchBar.ResignFirstResponder ();
		}
		[Export("updateSearchResultsForSearchController:")]
		public virtual void UpdateSearchResultsForSearchController(UISearchController searchController){
			Console.WriteLine ("serach prefom");
		}
        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}


