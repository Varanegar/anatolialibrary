using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Anatoli.App.Model.Product;
using Anatoli.App.Manager;
using AnatoliAndroid.ListAdapters;
using Anatoli.App.Model.AnatoliUser;
using Anatoli.App.Model;
using Anatoli.Framework.DataAdapter;
using Anatoli.Framework.AnatoliBase;
using AnatoliAndroid.Activities;
using System.Threading.Tasks;
using Android.Animation;
using Android.Views.InputMethods;
using Anatoli.App.Model.Store;
using AnatoliAndroid.Components;
using Anatoli.Framework;

namespace AnatoliAndroid.Fragments
{
    [FragmentTitle("سبد خرید")]
    class ShoppingCardFragment : Fragment
    {
        SwipeListView _itemsListView;
        TextView _itemCountTextView;
        ProductsListAdapter _listAdapter;
        RelativeLayout _countRelativeLayout;
        RelativeLayout _cardItemsRelativeLayout;
        TextView _deliveryAddress;
        TextView _factorePriceTextView;
        //TextView _deliveryTelTextView;
        TextView _storeTelTextView;
        TextView _countTextView;
        //TextView _nameTextView;
        Spinner _delivaryDate;
        Spinner _deliveryTime;
        //ImageView _editAddressImageView;
        ImageView _slideupmageView;
        ImageView _slidedownImageView;
        Button _checkoutButton;
        ImageButton _callImageButton;
        ImageButton _editAddressImageButton;
        DateOption[] _dateOptions;
        TimeOption[] _timeOptions;
        Spinner _typeSpinner;
        List<DeliveryTypeModel> _deliveryTypes;
        //Spinner _zoneSpinner;
        //Spinner _districtSpinner;
        //Spinner _citySpinner;
        //Spinner _provinceSpinner;
        bool _tomorrow = false;
        CustomerViewModel _customerViewModel;

        Cheesebaron.SlidingUpPanel.SlidingUpPanelLayout _slidingLayout;

        //ShoppingCardListToolsFragment _toolsDialog;

        public override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.ShoppingCardLayout, container, false);
            _itemsListView = view.FindViewById<SwipeListView>(Resource.Id.shoppingCardListView);
            _itemCountTextView = view.FindViewById<TextView>(Resource.Id.itemCountTextView);
            _countRelativeLayout = view.FindViewById<RelativeLayout>(Resource.Id.countRelativeLayout);
            _cardItemsRelativeLayout = view.FindViewById<RelativeLayout>(Resource.Id.cardItemsRelativeLayout);

            _checkoutButton = view.FindViewById<Button>(Resource.Id.checkoutButton);
            _checkoutButton.UpdateWidth();

            _slideupmageView = view.FindViewById<ImageView>(Resource.Id.slideupImageView);
            _slidedownImageView = view.FindViewById<ImageView>(Resource.Id.slidedownImageView);
            _slidedownImageView.Visibility = ViewStates.Gone;

            _slidingLayout = view.FindViewById<Cheesebaron.SlidingUpPanel.SlidingUpPanelLayout>(Resource.Id.sliding_layout);
            _slidingLayout.DragView = _countRelativeLayout;
            _slidingLayout.PanelExpanded += (s, e) =>
            {
                _cardItemsRelativeLayout.Visibility = ViewStates.Gone;
                _slidedownImageView.Visibility = ViewStates.Visible;
                _slideupmageView.Visibility = ViewStates.Gone;
            };
            _slidingLayout.PanelCollapsed += (s, e) =>
            {
                _cardItemsRelativeLayout.Visibility = ViewStates.Visible;
                _slidedownImageView.Visibility = ViewStates.Gone;
                _slideupmageView.Visibility = ViewStates.Visible;
            };
            _countTextView = view.FindViewById<TextView>(Resource.Id.itemCountTextView);
            _callImageButton = view.FindViewById<ImageButton>(Resource.Id.callImageButton);
            _storeTelTextView = view.FindViewById<TextView>(Resource.Id.storeTelTextView);
            _factorePriceTextView = view.FindViewById<TextView>(Resource.Id.factorPriceTextView);
            _deliveryAddress = view.FindViewById<TextView>(Resource.Id.addressTextView);
            _editAddressImageButton = view.FindViewById<ImageButton>(Resource.Id.editAddressImageButton);
            _delivaryDate = view.FindViewById<Spinner>(Resource.Id.dateSpinner);
            _deliveryTime = view.FindViewById<Spinner>(Resource.Id.timeSpinner);
            _typeSpinner = view.FindViewById<Spinner>(Resource.Id.typeSpinner);

            _checkoutButton.Click += async (s, e) =>
            {
                if (!AnatoliClient.GetInstance().WebClient.IsOnline())
                {
                    AlertDialog.Builder alertDialog = new AlertDialog.Builder(AnatoliApp.GetInstance().Activity);
                    alertDialog.SetMessage(Resource.String.PleaseConnectToInternet);
                    alertDialog.SetTitle(Resource.String.Error);
                    alertDialog.SetPositiveButton(Resource.String.Ok, delegate { });
                    alertDialog.Show();
                    return;
                }
                var store = await StoreManager.GetDefaultAsync();
                if (AnatoliApp.GetInstance().AnatoliUser == null)
                {
                    AlertDialog.Builder lAlert = new AlertDialog.Builder(AnatoliApp.GetInstance().Activity);
                    lAlert.SetMessage(Resource.String.PleaseLogin);
                    lAlert.SetPositiveButton(Resource.String.Ok, delegate
                    {
                        var transaction = AnatoliApp.GetInstance().Activity.FragmentManager.BeginTransaction();
                        var loginFragment = new LoginFragment();
                        loginFragment.Show(transaction, "shipping_dialog");
                    });
                    lAlert.SetNegativeButton(Resource.String.Cancel, delegate { });
                    lAlert.Show();
                    return;
                }
                if (await UpdateShippingInfo())
                {
                    ProgressDialog pDialog = new ProgressDialog(AnatoliApp.GetInstance().Activity);
                    try
                    {
                        if (_tomorrow)
                        {
                            AlertDialog.Builder lAlert = new AlertDialog.Builder(AnatoliApp.GetInstance().Activity);
                            lAlert.SetMessage("امکان ارسال برای امروز وجود ندارد. آیا مایل هستید سفارش شما فردا ارسال شود؟");
                            lAlert.SetPositiveButton(Resource.String.Yes, async delegate
                            {
                                pDialog.SetCancelable(false);
                                pDialog.SetMessage(AnatoliApp.GetResources().GetText(Resource.String.PleaseWait));
                                pDialog.SetTitle("در حال ارسال سفارش");
                                pDialog.Show();
                                // "BE2919AB-5564-447A-BE49-65A81E6AF712"
                                var o = await ShoppingCardManager.CalcPromo(_customerViewModel.UniqueId, store.store_id, _deliveryTypes[_typeSpinner.SelectedItemPosition].id);
                                pDialog.Dismiss();
                                if (o.IsValid)
                                {
                                    ProformaFragment proforma = new ProformaFragment(o, _customerViewModel);
                                    var fr = AnatoliApp.GetInstance().Activity.FragmentManager.BeginTransaction();
                                    proforma.ProformaAccepted += async delegate
                                    {
                                        ProgressDialog pDialog2 = new ProgressDialog(AnatoliApp.GetInstance().Activity);
                                        pDialog2.SetCancelable(false);
                                        pDialog2.SetMessage("سفارش شما در فاصله زمانی نیم ساعته به دست شما خواهد رسید");
                                        pDialog2.SetTitle("در حال ارسال سفارش");
                                        pDialog2.Show();
                                        try
                                        {
                                            var result = await ShoppingCardManager.Checkout(_customerViewModel.UniqueId, store.store_id, _deliveryTypes[_typeSpinner.SelectedItemPosition].id);
                                            pDialog2.Dismiss();
                                            if (result == null)
                                            {
                                                AlertDialog.Builder alert = new AlertDialog.Builder(AnatoliApp.GetInstance().Activity);
                                                alert.SetMessage("عدم دریافت اطلاعات از سرور");
                                                alert.SetTitle(Resource.String.Error);
                                                alert.SetNegativeButton(Resource.String.Ok, delegate { });
                                                alert.Show();
                                            }
                                            else if (result.IsValid)
                                            {
                                                await SaveOrder();
                                                await ShoppingCardManager.ClearAsync();
                                                proforma.Dismiss();
                                            }
                                            else
                                            {
                                                AlertDialog.Builder alert = new AlertDialog.Builder(AnatoliApp.GetInstance().Activity);
                                                alert.SetMessage("ارسال سفارش با مشکل مواجه شد");
                                                alert.SetTitle(Resource.String.Error);
                                                alert.SetNegativeButton(Resource.String.Ok, delegate { });
                                                alert.Show();
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            ex.SendTrace();
                                            AlertDialog.Builder alert = new AlertDialog.Builder(AnatoliApp.GetInstance().Activity);
                                            alert.SetMessage("ارسال سفارش با مشکل مواجه شد");
                                            alert.SetTitle(Resource.String.Error);
                                            alert.SetNegativeButton(Resource.String.Ok, delegate { });
                                            alert.Show();
                                        }
                                        finally
                                        {
                                            pDialog2.Dismiss();
                                        }
                                    };
                                    proforma.Show(fr, "proforma_fragment");
                                }
                                else
                                {
                                    AlertDialog.Builder alert = new AlertDialog.Builder(AnatoliApp.GetInstance().Activity);
                                    alert.SetMessage("ارسال سفارش با مشکل مواجه شد");
                                    alert.SetTitle(Resource.String.Error);
                                    alert.SetNegativeButton(Resource.String.Ok, delegate { });
                                    alert.Show();
                                }

                            });
                            lAlert.SetNegativeButton(Resource.String.Cancel, delegate
                            {
                                Toast.MakeText(AnatoliApp.GetInstance().Activity, "سفارش شما کنسل شد", ToastLength.Short).Show();
                                AnatoliApp.GetInstance().SetFragment<ProductsListFragment>(null, "products_fragment");
                            });
                            lAlert.Show();
                        }
                        else
                        {
                            pDialog.SetCancelable(false);
                            pDialog.SetMessage(AnatoliApp.GetResources().GetText(Resource.String.PleaseWait));
                            pDialog.SetTitle("در حال ارسال سفارش");
                            pDialog.Show();
                            var o = await ShoppingCardManager.CalcPromo(_customerViewModel.UniqueId, store.store_id, _deliveryTypes[_typeSpinner.SelectedItemPosition].id);
                            pDialog.Dismiss();
                            if (o.IsValid)
                            {
                                ProformaFragment proforma = new ProformaFragment(o, _customerViewModel);
                                var fr = AnatoliApp.GetInstance().Activity.FragmentManager.BeginTransaction();
                                proforma.ProformaAccepted += async delegate
                                {
                                    ProgressDialog pDialog2 = new ProgressDialog(AnatoliApp.GetInstance().Activity);
                                    pDialog2.SetCancelable(false);
                                    pDialog2.SetMessage("سفارش شما در فاصله زمانی نیم ساعته به دست شما خواهد رسید");
                                    pDialog2.SetTitle("در حال ارسال سفارش");
                                    pDialog2.Show();
                                    try
                                    {
                                        var result = await ShoppingCardManager.Checkout(_customerViewModel.UniqueId, store.store_id, _deliveryTypes[_typeSpinner.SelectedItemPosition].id);
                                        pDialog2.Dismiss();
                                        if (result == null)
                                        {
                                            AlertDialog.Builder alert = new AlertDialog.Builder(AnatoliApp.GetInstance().Activity);
                                            alert.SetMessage("عدم دریافت اطلاعات از سرور");
                                            alert.SetTitle(Resource.String.Error);
                                            alert.SetNegativeButton(Resource.String.Ok, delegate { });
                                            alert.Show();
                                        }
                                        else if (result.IsValid)
                                        {
                                            await SaveOrder();
                                            await ShoppingCardManager.ClearAsync();
                                            proforma.Dismiss();
                                        }
                                        else
                                        {
                                            AlertDialog.Builder alert = new AlertDialog.Builder(AnatoliApp.GetInstance().Activity);
                                            alert.SetMessage("ارسال سفارش با مشکل مواجه شد");
                                            alert.SetTitle(Resource.String.Error);
                                            alert.SetNegativeButton(Resource.String.Ok, delegate { });
                                            alert.Show();
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        ex.SendTrace();
                                        AlertDialog.Builder alert = new AlertDialog.Builder(AnatoliApp.GetInstance().Activity);
                                        alert.SetMessage("ارسال سفارش با مشکل مواجه شد");
                                        alert.SetTitle(Resource.String.Error);
                                        alert.SetNegativeButton(Resource.String.Ok, delegate { });
                                        alert.Show();
                                    }
                                    finally
                                    {
                                        pDialog2.Dismiss();
                                    }
                                };
                                proforma.Show(fr, "proforma_fragment");
                                pDialog.Dismiss();
                            }
                            else
                            {
                                AlertDialog.Builder alert = new AlertDialog.Builder(AnatoliApp.GetInstance().Activity);
                                alert.SetMessage("ارسال سفارش با مشکل مواجه شد");
                                alert.SetTitle(Resource.String.Error);
                                alert.SetNegativeButton(Resource.String.Ok, delegate { });
                                alert.Show();
                            }
                            pDialog.Dismiss();
                        }

                    }
                    catch (Exception ex)
                    {
                        ex.SendTrace();
                        AlertDialog.Builder alert = new AlertDialog.Builder(AnatoliApp.GetInstance().Activity);
                        alert.SetMessage("ارسال سفارش با مشکل مواجه شد");
                        alert.SetTitle(Resource.String.Error);
                        alert.SetNegativeButton(Resource.String.Ok, delegate { });
                        alert.Show();
                    }
                    finally
                    {
                        pDialog.Dismiss();
                    }

                }


            };
            //if (DateTime.Now.ToLocalTime().Hour < 16)
            //    _dateOptions = new DateOption[] { new DateOption("امروز", ShippingInfoManager.ShippingDateOptions.Today), new DateOption("فردا", ShippingInfoManager.ShippingDateOptions.Tommorow) };
            //else
            //    _dateOptions = new DateOption[] { new DateOption("فردا", ShippingInfoManager.ShippingDateOptions.Tommorow) };
            //_delivaryDate.Adapter = new ArrayAdapter(AnatoliApp.GetInstance().Activity, Android.Resource.Layout.SimpleListItem1, _dateOptions);
            //_delivaryDate.ItemSelected += (s, e) =>
            //{
            //    var selectedDateOption = _dateOptions[_delivaryDate.SelectedItemPosition];
            //    _timeOptions = ShippingInfoManager.GetAvailableDeliveryTimes(DateTime.Now.ToLocalTime(), selectedDateOption.date);
            //    _deliveryTime.Adapter = new ArrayAdapter(AnatoliApp.GetInstance().Activity, Android.Resource.Layout.SimpleListItem1, _timeOptions);
            //};

            _timeOptions = ShippingInfoManager.GetAvailableDeliveryTimes(DateTime.Now.ToLocalTime(), ShippingInfoManager.ShippingDateOptions.Today);
            if (_timeOptions.Length == 0)
            {
                _timeOptions = ShippingInfoManager.GetAvailableDeliveryTimes(DateTime.Now.ToLocalTime(), ShippingInfoManager.ShippingDateOptions.Tommorow);
                _tomorrow = true;
            }
            _deliveryTime.Adapter = new ArrayAdapter(AnatoliApp.GetInstance().Activity, Android.Resource.Layout.SimpleListItem1, _timeOptions);


            _editAddressImageButton.Click += (s, e) =>
            {
                var transaction = FragmentManager.BeginTransaction();
                EditShippingInfoFragment editShippingDialog = new EditShippingInfoFragment();
                editShippingDialog.SetAddress(_deliveryAddress.Text);
                //editShippingDialog.SetTel(_deliveryTelTextView.Text);
                //editShippingDialog.SetName(_nameTextView.Text);
                editShippingDialog.ShippingInfoChanged += (address, name, tel) =>
                {
                    _deliveryAddress.Text = address;
                    //_deliveryTelTextView.Text = tel;
                    //_nameTextView.Text = name;
                    _checkoutButton.Enabled = CheckCheckout();
                };
                editShippingDialog.Show(transaction, "shipping_dialog");

            };


            return view;
        }
        async Task SaveOrder()
        {
            try
            {
                var defaultStore = await StoreManager.GetDefaultAsync();
                if (defaultStore == null)
                {
                    Toast.MakeText(AnatoliApp.GetInstance().Activity, "ابتدا فروشگاه دلخواه را انتخاب نمایید", ToastLength.Short).Show();
                    var storeFrgment = AnatoliApp.GetInstance().SetFragment<StoresListFragment>(new StoresListFragment(), "stores_fragment");
                    await storeFrgment.RefreshAsync();
                    return;
                }
                await OrderManager.SaveOrder();
                OrderSavedDialogFragment dialog = new OrderSavedDialogFragment();
                var transaction = FragmentManager.BeginTransaction();
                dialog.Show(transaction, "order_saved_dialog");
                AnatoliApp.GetInstance().SetFragment<OrdersListFragment>(new OrdersListFragment(), "orders_fragment");
                AnatoliApp.GetInstance().ShoppingCardItemCount.Text = (await ShoppingCardManager.GetItemsCountAsync()).ToString();
                AnatoliApp.GetInstance().SetTotalPrice(await ShoppingCardManager.GetTotalPriceAsync());
            }
            catch (Exception ex)
            {
                ex.SendTrace();
            }
        }

        public async override void OnStart()
        {
            base.OnStart();
            AnatoliApp.GetInstance().HideMenuIcon();
            AnatoliApp.GetInstance().HideSearchIcon();

            try
            {
                var defaultStore = await StoreManager.GetDefaultAsync();
                if (defaultStore != null)
                {
                    string tel = defaultStore.store_tel;
                    if (String.IsNullOrEmpty(tel))
                    {
                        _storeTelTextView.Text = "نا مشخص";
                        _callImageButton.Visibility = ViewStates.Invisible;
                    }
                    else
                    {
                        _storeTelTextView.Text = tel;
                        _callImageButton.Visibility = ViewStates.Visible;
                        _callImageButton.Click += (s, e) =>
                        {
                            var uri = Android.Net.Uri.Parse(String.Format("tel:{0}", tel));
                            var intent = new Intent(Intent.ActionDial, uri);
                            StartActivity(intent);
                        };
                    }
                }
                else
                {
                    var storef = AnatoliApp.GetInstance().SetFragment<StoresListFragment>(new StoresListFragment(), "stores_fragment");
                    await storef.RefreshAsync();
                }

            }
            catch (Exception)
            {

            }

            _deliveryTypes = await BaseTypeManager.GetDeliveryTypesAsync();
            _typeSpinner.Adapter = new ArrayAdapter(AnatoliApp.GetInstance().Activity, Android.Resource.Layout.SimpleListItem1, _deliveryTypes);


            _factorePriceTextView.Text = (await ShoppingCardManager.GetTotalPriceAsync()).ToCurrency() + " تومان";
            _itemCountTextView.Text = (await ShoppingCardManager.GetItemsCountAsync()).ToString() + " عدد";
            _listAdapter = new ProductsListAdapter();
            _listAdapter.SwipeLeft += (s, p) => { _itemsListView.ShowOptions(p); };
            _listAdapter.SwipeRight += (s, p) => { _itemsListView.HideOptions(p); };
            _listAdapter.OptionsClick += (s, p) => { _itemsListView.HideOptions(p); };
            _listAdapter.List = await ShoppingCardManager.GetAllItemsAsync();
            _listAdapter.NotifyDataSetChanged();
            _listAdapter.DataChanged += async (s) =>
            {
                _factorePriceTextView.Text = (await ShoppingCardManager.GetTotalPriceAsync()).ToCurrency() + " تومان";
                _itemCountTextView.Text = (await ShoppingCardManager.GetItemsCountAsync()).ToString() + " عدد";
                _checkoutButton.Enabled = CheckCheckout();
            };
            _listAdapter.ShoppingCardItemRemoved += (s, item) =>
            {
                _listAdapter.List.Remove(item);
                _itemsListView.InvalidateViews();
                _checkoutButton.Enabled = CheckCheckout();
                Toast.MakeText(AnatoliAndroid.Activities.AnatoliApp.GetInstance().Activity, "حذف شد", ToastLength.Short).Show();
            };
            _itemsListView.Adapter = _listAdapter;
            if (_listAdapter.Count == 0)
            {
                Toast.MakeText(AnatoliAndroid.Activities.AnatoliApp.GetInstance().Activity, "سبد خرید خالی است", ToastLength.Short).Show();
                _checkoutButton.Enabled = CheckCheckout();
            }
            //_listAdapter.BackClick += async (s, p) =>
            //{
            //    await Task.Run(() => { (_itemsListView as SwipeListView).CloseAnimate(p); });
            //};

            await UpdateShippingInfo();

            _checkoutButton.Enabled = CheckCheckout();

            _factorePriceTextView.Text = (await ShoppingCardManager.GetTotalPriceAsync()).ToCurrency() + " تومان";
            _countTextView.Text = (await ShoppingCardManager.GetItemsCountAsync()).ToString() + " عدد";


        }

        async Task<bool> UpdateShippingInfo()
        {
            _customerViewModel = await CustomerManager.ReadCustomerAsync();
            if (_customerViewModel == null)
                _customerViewModel = await AnatoliApp.GetInstance().RefreshCutomerProfile();
            if (_customerViewModel != null)
            {
                _deliveryAddress.Text = _customerViewModel.MainStreet;
                if (String.IsNullOrEmpty(_customerViewModel.FirstName) || String.IsNullOrEmpty(_customerViewModel.LastName) || String.IsNullOrEmpty(_customerViewModel.NationalCode) || String.IsNullOrEmpty(_customerViewModel.MainStreet))
                {
                    AlertDialog.Builder lAlert = new AlertDialog.Builder(AnatoliApp.GetInstance().Activity);
                    lAlert.SetMessage("لطفا مشخصات خود را کامل کنید");
                    lAlert.SetPositiveButton(Resource.String.Ok, delegate
                    {
                        var transaction = AnatoliApp.GetInstance().Activity.FragmentManager.BeginTransaction();
                        var profileFragment = new ProfileFragment();
                        profileFragment.ProfileUpdated += async () => { await UpdateShippingInfo(); };
                        profileFragment.Show(transaction, "profile_fragment");
                    });
                    lAlert.Show();
                    return false;
                }
                return true;
            }
            return false;
        }
        bool CheckCheckout()
        {
            if (String.IsNullOrWhiteSpace(_deliveryAddress.Text) || String.IsNullOrEmpty(_deliveryAddress.Text) || _listAdapter.Count == 0)
                return false;
            else
                return true;
        }
    }
    public class OrderSavedDialogFragment : DialogFragment
    {
        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            var builder = new AlertDialog.Builder(Activity)
                .SetMessage("سفارش شما با موفقیت ثبت گردید. برای اطلاع از وضعیت سفارش خود به بخش پیغام ها یا سفارشات قبلی مراجعه نمایید")
                .SetPositiveButton("Ok", delegate
                {
                })
                .SetTitle("ثبت سفارش");
            return builder.Create();
        }
    }
}