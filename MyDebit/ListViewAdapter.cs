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

namespace MyDebit
{
    class ListViewAdapter : BaseAdapter
    {

        private Context context;
        private List<UserDebitDataModel> _userDebits;
        public ListViewAdapter(Context context, List<UserDebitDataModel> userDebit)
        {
            this.context = context;
            _userDebits = userDebit.Where(i => i.STATUS == false).ToList();
        }


        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;
            ListViewAdapterViewHolder holder = null;

            if (view != null)
                holder = view.Tag as ListViewAdapterViewHolder;

            if (holder == null)
            {
                holder = new ListViewAdapterViewHolder();
                var inflater = context.GetSystemService(Context.LayoutInflaterService).JavaCast<LayoutInflater>();
                //replace with your item and your holder items
                //comment back in
                view = inflater.Inflate(Resource.Layout.ListViewCustom, parent, false);
                holder.UserName = view.FindViewById<TextView>(Resource.Id.tv_name);
                holder.DateTime = view.FindViewById<TextView>(Resource.Id.tv_date);
                holder.Money = view.FindViewById<TextView>(Resource.Id.tv_money);
                holder.DebitStatus = view.FindViewById<ImageView>(Resource.Id.image_paid);

                view.Tag = holder;
            }


            //fill in your items
            string moneyFormat = new SangUtil().ConvertIntToCurrency(_userDebits[position].MONEY);

            holder.UserName.Text = _userDebits[position].NAME;
            holder.Money.Text = moneyFormat;
            holder.DateTime.Text = _userDebits[position].DATE.ToShortDateString();

            holder.DebitStatus.Click += (sender, e) =>
            {
                UpdateDebitStatus(position);
            };

            return view;
        }

        readonly SangUtil.RestApiRequest _client  = new SangUtil.RestApiRequest();
        private void UpdateDebitStatus(int position)
        {
            var itemSelected = _userDebits.ElementAt(position);

            itemSelected.STATUS = true;

            var updateResult = _client.PutMethod("MY_DEBIT/", itemSelected.ID.ToString(), itemSelected);
            if (updateResult.Equals("OK"))
            {
                ReloadPage();
            }
        }
        private void ReloadPage()
        {
            var intent = new Intent(context, typeof(MainActivity));
            intent.SetFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask);
            context.StartActivity(intent);
        }
        public override int Count
        {
            get
            {
                return _userDebits.Count;
            }
        }

    }

    class ListViewAdapterViewHolder : Java.Lang.Object
    {
        //Your adapter views to re-use
        //public TextView Title { get; set; }

        public TextView UserName { get; set; }
        public TextView Money { get; set; }
        public TextView DateTime { get; set; }

        public ImageView DebitStatus { get; set; }
    }
}