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
using Anatoli.App.Model.Store;
using Anatoli.Framework.AnatoliBase;
using AnatoliAndroid.Activities;

namespace AnatoliAndroid.Fragments
{
    public class ProformaFragment : DialogFragment
    {
        PurchaseOrderViewModel _orderViewModel;
        public ProformaFragment(PurchaseOrderViewModel orderViewModel)
        {
            _orderViewModel = orderViewModel;
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.ProformaLayout, container, false);
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);

            view.FindViewById<TextView>(Resource.Id.deliveryAddressTextView).Text = "شعبه شریعتی";
            view.FindViewById<TextView>(Resource.Id.orderNumberTextView).Text = _orderViewModel.UniqueId;
            view.FindViewById<TextView>(Resource.Id.orderDateTextView).Text = _orderViewModel.OrderDate.ToString();
            view.FindViewById<TextView>(Resource.Id.orderPriceTextView).Text = _orderViewModel.FinalAmount.ToCurrency();
            ListView itemsListView = view.FindViewById<ListView>(Resource.Id.itemsListView);
            itemsListView.Adapter = new ProformaListAdapter(AnatoliApp.GetInstance().Activity, _orderViewModel.LineItems,this);

            return view;
        }

        public class ProformaListAdapter : BaseAdapter<PurchaseOrderLineItemViewModel>
        {
            List<PurchaseOrderLineItemViewModel> _list;
            Activity _context;
            ProformaFragment _fragment;
            public ProformaListAdapter(Activity context, List<PurchaseOrderLineItemViewModel> list, ProformaFragment fragment)
            {
                _list = list;
                _context = context;
                _fragment = fragment;
            }
            public override int Count
            {
                get { return _list.Count; }
            }

            public override long GetItemId(int position)
            {
                return position;
            }

            public override View GetView(int position, View convertView, ViewGroup parent)
            {
                var view = _context.LayoutInflater.Inflate(Resource.Layout.SimpleOrderItemLayout, null);
                var item = _list[position];
                view.FindViewById<TextView>(Resource.Id.itemNameTextView).Text = item.UniqueId;
                view.FindViewById<TextView>(Resource.Id.itemCountTextView).Text = item.Qty.ToString();
                view.FindViewById<TextView>(Resource.Id.itemPriceTextView).Text = item.FinalNetAmount.ToCurrency();
                var button = view.FindViewById<Button>(Resource.Id.okButton);
                button.UpdateWidth();
                button.Click += (s, e) =>
                {
                    _fragment.OnProformaAccepted();
                };
                return view;
            }

            public override PurchaseOrderLineItemViewModel this[int position]
            {
                get { return (_list[position] != null) ? _list[position] : null; }
            }
        }

        void OnProformaAccepted()
        {
            if (ProformaAccepted != null)
            {
                ProformaAccepted.Invoke(this, new EventArgs());
            }
        }
        public event EventHandler ProformaAccepted;
    }
}