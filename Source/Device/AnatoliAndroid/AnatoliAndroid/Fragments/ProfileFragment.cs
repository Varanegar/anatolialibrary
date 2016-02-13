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
using Android.Graphics;
using System.Net;
using System.Threading.Tasks;
using Android.Provider;
using Android.Database;
using AnatoliAndroid.Components;
using Square.Picasso;

namespace AnatoliAndroid.Fragments
{
    [FragmentTitle("مشخصات من")]
    public class ProfileFragment : DialogFragment
    {
        EditText _firstNameEditText;
        EditText _lastNameEditText;
        EditText _emailEditText;
        TextView _telTextView;
        EditText _addressEditText;
        EditText _idEditText;
        Spinner _level3Spinner;
        Spinner _level4Spinner;
        Spinner _level2Spinner;
        Spinner _level1Spinner;
        TextView _exitTextView;
        TextView _fullNametextView;
        Button _saveButton;
        RoundedImageView _avatarImageView;
        CustomerViewModel _customerViewModel;
        ProgressBar _progress;
        ImageView _cancelImageView;
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
            _telTextView = view.FindViewById<TextView>(Resource.Id.telTextView);
            _addressEditText = view.FindViewById<EditText>(Resource.Id.addressEditText);
            _level3Spinner = view.FindViewById<Spinner>(Resource.Id.level4Spinner);
            _level4Spinner = view.FindViewById<Spinner>(Resource.Id.level3Spinner);
            _level2Spinner = view.FindViewById<Spinner>(Resource.Id.level2Spinner);
            _level1Spinner = view.FindViewById<Spinner>(Resource.Id.level1Spinner);
            _avatarImageView = view.FindViewById<RoundedImageView>(Resource.Id.avatarImageView);
            _progress = view.FindViewById<ProgressBar>(Resource.Id.progress);
            _cancelImageView = view.FindViewById<ImageView>(Resource.Id.cancelImageView);

            ImageUploaded += (s, e) =>
            {
                var imageUri = CustomerManager.GetImageAddress(_customerViewModel.UniqueId);
                Picasso.With(AnatoliApp.GetInstance().Activity).Load(imageUri).MemoryPolicy(MemoryPolicy.NoCache).NetworkPolicy(NetworkPolicy.NoCache).Placeholder(Resource.Drawable.ic_account_circle_white_24dp).Into(_avatarImageView);
                Toast.MakeText(AnatoliApp.GetInstance().Activity, "تصویر ارسال شد", ToastLength.Short).Show();
                _progress.Visibility = ViewStates.Gone;
            };
            ImageUploadFailed += (s, e) =>
            {
                Picasso.With(AnatoliApp.GetInstance().Activity).Load(CustomerManager.GetImageAddress(_customerViewModel.UniqueId)).Placeholder(Resource.Drawable.ic_account_circle_white_24dp).Into(_avatarImageView);
                Toast.MakeText(AnatoliApp.GetInstance().Activity, "خطا در ارسال تصویر", ToastLength.Short).Show();
                _progress.Visibility = ViewStates.Gone;
            };

            _avatarImageView.Click += (s, e) =>
            {
                OpenImage();
            };
            _fullNametextView = view.FindViewById<TextView>(Resource.Id.fullNametextView);
            view.FindViewById<TextView>(Resource.Id.changePassTextView).Click += (s, e) =>
            {
                ChangePassFragment fragment = new ChangePassFragment();
                var transaction = AnatoliApp.GetInstance().Activity.FragmentManager.BeginTransaction();
                fragment.Show(transaction, "changepass_fragment");
                Dismiss();
            };

            _saveButton = view.FindViewById<Button>(Resource.Id.saveButton);
            _saveButton.UpdateWidth();

            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);



            _saveButton.Click += async (s, e) =>
            {
                if (!IsValidEmail(_emailEditText.Text))
                {
                    AlertDialog.Builder alert = new AlertDialog.Builder(AnatoliApp.GetInstance().Activity);
                    alert.SetTitle(Resource.String.Error);
                    alert.SetMessage(Resource.String.PleaseEnterValidEmail);
                    alert.Show();
                    return;
                }
                if (String.IsNullOrEmpty(_idEditText.Text) || String.IsNullOrEmpty(_firstNameEditText.Text) || String.IsNullOrEmpty(_lastNameEditText.Text) || String.IsNullOrEmpty(_addressEditText.Text))
                {
                    AlertDialog.Builder alert = new AlertDialog.Builder(AnatoliApp.GetInstance().Activity);
                    alert.SetTitle(Resource.String.Error);
                    alert.SetMessage("ورود نام، نام خانوادگی، شماره ملی و آدرس الزامی است");
                    alert.Show();
                    return;
                }
                _customerViewModel.MainStreet = _addressEditText.Text;
                _customerViewModel.Email = _emailEditText.Text;
                _customerViewModel.NationalCode = _idEditText.Text;
                _customerViewModel.FirstName = _firstNameEditText.Text;
                _customerViewModel.LastName = _lastNameEditText.Text;
                _customerViewModel.RegionLevel1Id = _level1SpinerDataAdapter[_level1Spinner.SelectedItemPosition].group_id;
                _customerViewModel.RegionLevel2Id = _level2SpinerDataAdapter[_level2Spinner.SelectedItemPosition].group_id;
                _customerViewModel.RegionLevel3Id = _level3SpinerDataAdapter[_level3Spinner.SelectedItemPosition].group_id;
                _customerViewModel.RegionLevel4Id = _level4SpinerDataAdapter[_level4Spinner.SelectedItemPosition].group_id;
                AlertDialog.Builder dialog = new AlertDialog.Builder(AnatoliApp.GetInstance().Activity);
                if (!AnatoliClient.GetInstance().WebClient.IsOnline())
                {
                    dialog.SetTitle(Resources.GetText(Resource.String.NetworkAccessFailed));
                    dialog.SetMessage(Resources.GetText(Resource.String.PleaseConnectToInternet));
                    dialog.SetPositiveButton(Resource.String.Ok, (s2, e2) =>
                    {
                        Intent intent = new Intent(Android.Provider.Settings.ActionSettings);
                        AnatoliApp.GetInstance().Activity.StartActivity(intent);
                    });
                    dialog.SetNegativeButton(Resource.String.Cancel, (s2, e2) => { });
                    dialog.Show();
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
                        dialog.SetTitle("");
                        dialog.SetMessage("اطلاعات بروزرسانی شد");
                        dialog.SetPositiveButton(Resource.String.Ok, (s2, e2) => { });
                        dialog.Show();
                        OnProfileUpdated();
                        Dismiss();
                    }
                    else
                    {
                        dialog.SetTitle("خطا");
                        dialog.SetMessage(result.ModelStateString);
                        dialog.SetPositiveButton(Resource.String.Ok, (s2, e2) => { });
                        dialog.Show();
                    }
                }
                catch (Exception ex)
                {
                    HockeyApp.TraceWriter.WriteTrace(ex, false);
                    pDialog.Dismiss();
                    dialog.SetMessage(Resource.String.ErrorOccured);
                    dialog.SetTitle("خطا");
                    dialog.SetPositiveButton(Resource.String.Ok, (s2, e2) => { });
                    dialog.Show();
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
                        Dismiss();
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
                    _telTextView.Text = _customerViewModel.Mobile;
                    _fullNametextView.Text = _customerViewModel.FirstName.Trim() + " " + _customerViewModel.LastName.Trim();

                    Picasso.With(AnatoliApp.GetInstance().Activity).Load(CustomerManager.GetImageAddress(_customerViewModel.UniqueId)).Placeholder(Resource.Drawable.ic_account_circle_white_24dp).Into(_avatarImageView);
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
            catch (Exception ex)
            {
                HockeyApp.TraceWriter.WriteTrace(ex, false);
            }

        }
        public static bool IsValidEmail(string target)
        {
            if (target == null)
            {
                return false;
            }
            else
            {
                return Android.Util.Patterns.EmailAddress.Matcher(target).Matches();
            }
        }
        public static bool IsValidPhoneNumber(string target)
        {
            if (target == null)
            {
                return false;
            }
            else
            {
                return Android.Util.Patterns.Phone.Matcher(target).Matches();
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
        public static readonly int OpenImageRequestCode = 1234;
        public override async void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == OpenImageRequestCode)
            {
                try
                {
                    _progress.Visibility = ViewStates.Visible;
                    _cancelImageView.Visibility = ViewStates.Visible;
                    _avatarImageView.Enabled = false;
                    if (data.Data != null)
                    {
                        var path = AndroidFileIO.GetPathToImage(data.Data, AnatoliApp.GetInstance().Activity);
                        var image = AnatoliClient.GetInstance().FileIO.ReadAllBytes(path);
                        System.Threading.CancellationTokenSource cancelToken = new System.Threading.CancellationTokenSource();
                        _cancelImageView.Click += delegate
                        {
                            cancelToken.Cancel();
                            _progress.Visibility = ViewStates.Gone;
                            _cancelImageView.Visibility = ViewStates.Gone;
                        };
                        await CustomerManager.UploadImageAsync(_customerViewModel.UniqueId, image, cancelToken);
                        OnImageUploaded();
                    }
                }
                catch (Exception e)
                {
                    HockeyApp.TraceWriter.WriteTrace(e, false);
                    if (e.GetType() != typeof(TaskCanceledException))
                    {
                        OnImageUploadFailed();
                    }
                }
                finally
                {
                    _progress.Visibility = ViewStates.Gone;
                    _cancelImageView.Visibility = ViewStates.Gone;
                    _avatarImageView.Enabled = true;
                }
            }
        }
        void OnImageUploaded()
        {
            if (ImageUploaded != null)
            {
                ImageUploaded.Invoke(this, new EventArgs());
            }
        }
        public EventHandler ImageUploaded;

        void OnImageUploadFailed()
        {
            if (ImageUploadFailed != null)
            {
                ImageUploadFailed.Invoke(this, new EventArgs());
            }
        }
        public EventHandler ImageUploadFailed;
        public void OpenImage()
        {
            try
            {
                Intent intent = new Intent(Intent.ActionGetContent);
                intent.SetType("image/*");
                StartActivityForResult(intent, OpenImageRequestCode);
            }
            catch (ActivityNotFoundException e)
            {

            }
        }

        void OnProfileUpdated()
        {
            if (ProfileUpdated != null)
            {
                ProfileUpdated.Invoke();
            }
        }
        public event ProfileUpdatedEventHandler ProfileUpdated;
        public delegate void ProfileUpdatedEventHandler();
    }
}