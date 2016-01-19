using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Anatoli.App.Manager;
using Anatoli.App.Model.AnatoliUser;
using AnatoliAndroid.Activities;
using Anatoli.Framework.AnatoliBase;
using Anatoli.App.Model;
using Anatoli.App.Model.Store;

namespace AnatoliAndroid.Fragments
{
    [FragmentTitle("مشخصات من")]
    public class ProfileFragment : DialogFragment
    {
        EditText _firstNameEditText;
        EditText _lastNameEditText;
        EditText _emailEditText;
        EditText _telEditText;
        EditText _addressEditText;
        EditText _idEditText;
        Spinner _level3Spinner;
        Spinner _level4Spinner;
        Spinner _level2Spinner;
        Spinner _level1Spinner;
        TextView _exitTextView;
        Button _saveButton;
        CustomerViewModel _customerViewModel;
        List<CityRegionModel> _level1SpinerDataAdapter;
        List<CityRegionModel> _level2SpinerDataAdapter = new List<CityRegionModel>();
        List<CityRegionModel> _level3SpinerDataAdapter = new List<CityRegionModel>();
        List<CityRegionModel> _level4SpinerDataAdapter = new List<CityRegionModel>();

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.ProfileLayout, container, false);
            _firstNameEditText = view.FindViewById<EditText>(Resource.Id.firstNameEditText);
            _lastNameEditText = view.FindViewById<EditText>(Resource.Id.lastNameEditText);
            _emailEditText = view.FindViewById<EditText>(Resource.Id.emailEditText);
            _idEditText = view.FindViewById<EditText>(Resource.Id.idEditText);
            _telEditText = view.FindViewById<EditText>(Resource.Id.telEditText);
            _addressEditText = view.FindViewById<EditText>(Resource.Id.addressEditText);
            _level3Spinner = view.FindViewById<Spinner>(Resource.Id.level4Spinner);
            _level4Spinner = view.FindViewById<Spinner>(Resource.Id.level3Spinner);
            _level2Spinner = view.FindViewById<Spinner>(Resource.Id.level2Spinner);
            _level1Spinner = view.FindViewById<Spinner>(Resource.Id.level1Spinner);
            _saveButton = view.FindViewById<Button>(Resource.Id.saveButton);
            _saveButton.UpdateWidth();

            _telEditText.Enabled = false;
            _saveButton.Click += async (s, e) =>
            {
                _customerViewModel.MainStreet = _addressEditText.Text;
                _customerViewModel.Email = _emailEditText.Text;
                _customerViewModel.NationalCode = _idEditText.Text;
                _customerViewModel.FirstName = _firstNameEditText.Text;
                _customerViewModel.LastName = _lastNameEditText.Text;
                _customerViewModel.RegionLevel1Id = _level1SpinerDataAdapter[_level1Spinner.SelectedItemPosition].group_id;
                _customerViewModel.RegionLevel2Id = _level2SpinerDataAdapter[_level2Spinner.SelectedItemPosition].group_id;
                _customerViewModel.RegionLevel3Id = _level3SpinerDataAdapter[_level3Spinner.SelectedItemPosition].group_id;
                _customerViewModel.RegionLevel4Id = _level4SpinerDataAdapter[_level4Spinner.SelectedItemPosition].group_id;
                AlertDialog.Builder errDialog = new AlertDialog.Builder(AnatoliApp.GetInstance().Activity);
                if (!AnatoliClient.GetInstance().WebClient.IsOnline())
                {
                    errDialog.SetTitle(Resources.GetText(Resource.String.NetworkAccessFailed));
                    errDialog.SetMessage(Resources.GetText(Resource.String.PleaseConnectToInternet));
                    errDialog.SetPositiveButton(Resource.String.Ok, (s2, e2) =>
                    {
                        Intent intent = new Intent(Android.Provider.Settings.ActionSettings);
                        AnatoliApp.GetInstance().Activity.StartActivity(intent);
                    });
                    errDialog.SetNegativeButton(Resource.String.Cancel, (s2, e2) => { });
                    errDialog.Show();
                    return;
                }
                ProgressDialog pDialog = new ProgressDialog(AnatoliApp.GetInstance().Activity);
                try
                {
                    pDialog.SetTitle(AnatoliApp.GetResources().GetText(Resource.String.Updating));
                    pDialog.SetMessage(AnatoliApp.GetResources().GetText(Resource.String.PleaseWait));
                    pDialog.Show();
                    var result = await CustomerManager.UploadCustomerAsync(_customerViewModel);
                    pDialog.Dismiss();
                    if (result.IsValid)
                    {
                        await CustomerManager.SaveCustomerAsync(_customerViewModel);
                        errDialog.SetTitle("");
                        errDialog.SetMessage("اطلاعات بروزرسانی شد");
                        errDialog.SetPositiveButton(Resource.String.Ok, (s2, e2) => { });
                        errDialog.Show();
                    }
                    else
                    {
                        errDialog.SetTitle("خطا");
                        errDialog.SetMessage(result.ModelStateString);
                        errDialog.SetPositiveButton(Resource.String.Ok, (s2, e2) => { });
                        errDialog.Show();
                    }
                }
                catch (Exception ex)
                {
                    pDialog.Dismiss();
                    errDialog.SetMessage(Resource.String.ErrorOccured);
                    errDialog.SetTitle("خطا");
                    errDialog.SetPositiveButton(Resource.String.Ok, (s2, e2) => { });
                    errDialog.Show();
                }
            };
            _exitTextView = view.FindViewById<TextView>(Resource.Id.logoutTextView);
            _exitTextView.Click += (s, e) =>
            {
                AlertDialog.Builder alert = new AlertDialog.Builder(AnatoliApp.GetInstance().Activity);
                alert.SetMessage(AnatoliApp.GetResources().GetText(Resource.String.AreYouSure));
                alert.SetPositiveButton(AnatoliApp.GetResources().GetText(Resource.String.Yes), async (s2, e2) =>
                {
                    bool result = await AnatoliUserManager.LogoutAsync();
                    if (result)
                    {
                        AnatoliApp.GetInstance().AnatoliUser = null;
                        AnatoliApp.GetInstance().RefreshMenuItems();
                        AnatoliApp.GetInstance().SetFragment<FirstFragment>(null, "first_fragment");
                    }

                });
                alert.SetNegativeButton(AnatoliApp.GetResources().GetText(Resource.String.No), (s2, e2) => { });
                alert.Show();
            };
            return view;
        }
        public async override void OnStart()
        {
            base.OnStart();
            AnatoliApp.GetInstance().HideMenuIcon();
            AnatoliApp.GetInstance().HideSearchIcon();
            // manipulate spin boxes
            _level1SpinerDataAdapter = await CityRegionManager.GetFirstLevelAsync();
            _level1Spinner.Adapter = new ArrayAdapter<CityRegionModel>(AnatoliApp.GetInstance().Activity, Android.Resource.Layout.SimpleListItem1, _level1SpinerDataAdapter);
            _level1Spinner.ItemSelected += _level1Spinner_ItemSelected;
            _level2Spinner.ItemSelected += _level2Spinner_ItemSelected;
            _level3Spinner.ItemSelected += _level3Spinner_ItemSelected;
            try
            {
                if (_customerViewModel == null)
                    _customerViewModel = await CustomerManager.ReadCustomerAsync();
                if (_customerViewModel == null)
                    _customerViewModel = await AnatoliApp.GetInstance().RefreshCutomerProfile(true);
                if (_customerViewModel != null)
                {
                    _firstNameEditText.Text = _customerViewModel.FirstName;
                    _lastNameEditText.Text = _customerViewModel.LastName;
                    _idEditText.Text = _customerViewModel.NationalCode;
                    _addressEditText.Text = _customerViewModel.MainStreet;
                    _emailEditText.Text = _customerViewModel.Email;
                    _telEditText.Text = _customerViewModel.Mobile;
                    _level1Spinner.ItemSelected -= _level1Spinner_ItemSelected;
                    for (int i = 0; i < _level1SpinerDataAdapter.Count; i++)
                    {
                        if (_level1SpinerDataAdapter[i].group_id == _customerViewModel.RegionLevel1Id)
                        {
                            _level1Spinner.SetSelection(i);
                            break;
                        }
                    }
                    CityRegionModel selectedItem = _level1SpinerDataAdapter[_level1Spinner.SelectedItemPosition];
                    _level2SpinerDataAdapter = await CityRegionManager.GetGroupsAsync(selectedItem.group_id);
                    _level2Spinner.Adapter = new ArrayAdapter<CityRegionModel>(AnatoliApp.GetInstance().Activity, Android.Resource.Layout.SimpleListItem1, _level2SpinerDataAdapter);
                    _level2Spinner.ItemSelected -= _level2Spinner_ItemSelected;
                    for (int i = 0; i < _level2SpinerDataAdapter.Count; i++)
                    {
                        if (_level2SpinerDataAdapter[i].group_id == _customerViewModel.RegionLevel2Id)
                        {
                            _level2Spinner.SetSelection(i);
                        }
                    }

                    selectedItem = _level2SpinerDataAdapter[_level2Spinner.SelectedItemPosition];
                    _level3SpinerDataAdapter = await CityRegionManager.GetGroupsAsync(selectedItem.group_id);
                    _level3Spinner.Adapter = new ArrayAdapter<CityRegionModel>(AnatoliApp.GetInstance().Activity, Android.Resource.Layout.SimpleListItem1, _level3SpinerDataAdapter);
                    _level3Spinner.ItemSelected -= _level3Spinner_ItemSelected;
                    for (int i = 0; i < _level3SpinerDataAdapter.Count; i++)
                    {
                        if (_level3SpinerDataAdapter[i].group_id == _customerViewModel.RegionLevel3Id)
                        {
                            _level3Spinner.SetSelection(i);
                        }
                    }

                    selectedItem = _level3SpinerDataAdapter[_level3Spinner.SelectedItemPosition];
                    _level4SpinerDataAdapter = await CityRegionManager.GetGroupsAsync(selectedItem.group_id);
                    _level4Spinner.Adapter = new ArrayAdapter<CityRegionModel>(AnatoliApp.GetInstance().Activity, Android.Resource.Layout.SimpleListItem1, _level4SpinerDataAdapter);


                    for (int i = 0; i < _level4SpinerDataAdapter.Count; i++)
                    {
                        if (_level4SpinerDataAdapter[i].group_id == _customerViewModel.RegionLevel4Id)
                        {
                            _level4Spinner.SetSelection(i);
                        }
                    }

                    _level1Spinner.ItemSelected += _level1Spinner_ItemSelected;
                    _level2Spinner.ItemSelected += _level2Spinner_ItemSelected;
                    _level3Spinner.ItemSelected += _level3Spinner_ItemSelected;
                }
            }
            catch (Exception)
            {

            }

        }

        async void _level2Spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            try
            {
                CityRegionModel selectedItem = _level2SpinerDataAdapter[e.Position];
                _level3SpinerDataAdapter = await CityRegionManager.GetGroupsAsync(selectedItem.group_id);
                _level3Spinner.Adapter = new ArrayAdapter<CityRegionModel>(AnatoliApp.GetInstance().Activity, Android.Resource.Layout.SimpleListItem1, _level3SpinerDataAdapter);
            }
            catch (Exception)
            {

            }
        }

        async void _level3Spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            try
            {
                CityRegionModel selectedItem = _level3SpinerDataAdapter[e.Position];
                _level4SpinerDataAdapter = await CityRegionManager.GetGroupsAsync(selectedItem.group_id);
                _level4Spinner.Adapter = new ArrayAdapter<CityRegionModel>(AnatoliApp.GetInstance().Activity, Android.Resource.Layout.SimpleListItem1, _level4SpinerDataAdapter);
            }
            catch (Exception)
            {

            }
        }

        async void _level1Spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {

            try
            {
                CityRegionModel selectedItem = _level1SpinerDataAdapter[e.Position];
                _level2SpinerDataAdapter = await CityRegionManager.GetGroupsAsync(selectedItem.group_id);
                _level2Spinner.Adapter = new ArrayAdapter<CityRegionModel>(AnatoliApp.GetInstance().Activity, Android.Resource.Layout.SimpleListItem1, _level2SpinerDataAdapter);
            }
            catch (Exception)
            {

            }
        }
    }
}