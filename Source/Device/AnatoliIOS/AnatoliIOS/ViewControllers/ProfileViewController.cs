using System;

using UIKit;
using CoreGraphics;
using Anatoli.App.Model.Store;
using System.Collections.Generic;
using Anatoli.App.Manager;
using Anatoli.App.Model;
using AnatoliIOS.Components;

namespace AnatoliIOS.ViewControllers
{
    public partial class ProfileViewController : BaseController
    {
        public ProfileViewController()
            : base("ProfileViewController", null)
        {
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        public async override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Perform any additional setup after loading the view, typically from a nib.
            View.Bounds = UIScreen.MainScreen.Bounds;
            if (AnatoliApp.GetInstance().Customer != null)
            {
                nameTextField.Text = AnatoliApp.GetInstance().Customer.FirstName;
                lastNameTextField.Text = AnatoliApp.GetInstance().Customer.LastName;
                emailTextField.Text = AnatoliApp.GetInstance().Customer.Email;
                addressTextField.Text = AnatoliApp.GetInstance().Customer.MainStreet;
            }



            var level1PickerModel = new CityRegionPickerViewModel(await CityRegionManager.GetFirstLevelAsync());
            var level2PickerModel = new CityRegionPickerViewModel(await CityRegionManager.GetGroupsAsync(AnatoliApp.GetInstance().Customer.RegionLevel1Id));
            var level3PickerModel = new CityRegionPickerViewModel(await CityRegionManager.GetGroupsAsync(AnatoliApp.GetInstance().Customer.RegionLevel2Id));
            var level4PickerModel = new CityRegionPickerViewModel(await CityRegionManager.GetGroupsAsync(AnatoliApp.GetInstance().Customer.RegionLevel3Id));

            level1Picker.Model = level1PickerModel;
            level2Picker.Model = level2PickerModel;
            level3Picker.Model = level3PickerModel;
            level4Picker.Model = level4PickerModel;

            SelectByGroupId(level1Picker, AnatoliApp.GetInstance().Customer.RegionLevel1Id);
            SelectByGroupId(level2Picker, AnatoliApp.GetInstance().Customer.RegionLevel2Id);
            SelectByGroupId(level3Picker, AnatoliApp.GetInstance().Customer.RegionLevel3Id);
            SelectByGroupId(level4Picker, AnatoliApp.GetInstance().Customer.RegionLevel4Id);

            level1PickerModel.ItemSelected += async (item) =>
            {
                if (item != null)
                {
                    (level2Picker.Model as CityRegionPickerViewModel).SetItems(await CityRegionManager.GetGroupsAsync(item.group_id));
                    level2Picker.ReloadComponent(0);
                }
                level2Picker.Select(0, 0, true);
                level3Picker.Select(0, 0, true);
                level4Picker.Select(0, 0, true);
            };
            level2PickerModel.ItemSelected += async (item) =>
            {
                if (item != null)
                {
                    (level3Picker.Model as CityRegionPickerViewModel).SetItems(await CityRegionManager.GetGroupsAsync(item.group_id));
                    level3Picker.ReloadComponent(0);
                }
                level3Picker.Select(0, 0, true);
                level4Picker.Select(0, 0, true);
            };
            level3PickerModel.ItemSelected += async (item) =>
            {
                if (item != null)
                {
                    (level4Picker.Model as CityRegionPickerViewModel).SetItems(await CityRegionManager.GetGroupsAsync(item.group_id));
                    level4Picker.ReloadComponent(0);
                }
                level4Picker.Select(0, 0, true);
            };

            saveButton.TouchUpInside += async (object sender, EventArgs e) =>
            {
                CustomerViewModel customer = new CustomerViewModel();
                customer.Address = addressTextField.Text;
                customer.MainStreet = addressTextField.Text;
                customer.LastName = lastNameTextField.Text;
                customer.FirstName = nameTextField.Text;
                customer.Email = emailTextField.Text;
                LoadingOverlay loadingOverlay;
                var bounds = UIScreen.MainScreen.Bounds;
                loadingOverlay = new LoadingOverlay(bounds);
                View.Add(loadingOverlay);
                var result = await CustomerManager.UploadCustomerAsync(customer);
                if (result != null)
                {
                    if (result.IsValid)
                    {
                        try
                        {
                            await CustomerManager.DownloadCustomerAsync(AnatoliApp.GetInstance().User, null);
                            AnatoliApp.GetInstance().Customer = await CustomerManager.ReadCustomerAsync();
                            var alert = UIAlertController.Create("", "اطلاعات شما دخیره شد", UIAlertControllerStyle.Alert);
                            alert.AddAction(UIAlertAction.Create("خب", UIAlertActionStyle.Default, delegate { AnatoliApp.GetInstance().PushViewController(new FirstPageViewController()); }));
                            PresentViewController(alert, true, null);
                        }
                        catch (Exception)
                        {
                            var alert = UIAlertController.Create("", "خطا در ذخیره اطلاعات", UIAlertControllerStyle.Alert);
                            alert.AddAction(UIAlertAction.Create("خب", UIAlertActionStyle.Default, null));
                            PresentViewController(alert, true, null);
                        }
                    }
                }
                loadingOverlay.Hide();

            };

        }
        public void SelectByGroupId(UIPickerView picker, string groupId)
        {
            var regionPicker = (picker.Model as CityRegionPickerViewModel);
            if (regionPicker.ItemsDictionary.ContainsKey(groupId))
            {
                picker.Select(regionPicker.ItemsDictionary[groupId], 0, true);
            }
        }
    }

    class CityRegionPickerViewModel : UIPickerViewModel
    {
        List<CityRegionModel> _items;
        public Dictionary<string, int> ItemsDictionary;
        public void SetItems(List<CityRegionModel> items)
        {
            _items = items;
            _items.Insert(0, null);
            ItemsDictionary = new Dictionary<string, int>();
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i] != null)
                {
                    ItemsDictionary.Add(_items[i].group_id, i);
                }
            }
        }
        public CityRegionPickerViewModel(List<CityRegionModel> items)
        {
            SetItems(items);
        }

        public override nint GetComponentCount(UIPickerView pickerView)
        {
            return 1;
        }
        public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
        {
            return _items.Count;
        }
        public override string GetTitle(UIPickerView pickerView, nint row, nint component)
        {
            if (_items[(int)row] != null)
                return _items[(int)row].group_name;
            else
                return "";
        }

        public override void Selected(UIPickerView pickerView, nint row, nint component)
        {
            OnSelected(_items[(int)row]);
        }
        void OnSelected(CityRegionModel item)
        {
            if (ItemSelected != null)
            {
                ItemSelected.Invoke(item);
            }
        }
        public event ItemSelectedEventHandler ItemSelected;
        public delegate void ItemSelectedEventHandler(CityRegionModel item);
    }

}