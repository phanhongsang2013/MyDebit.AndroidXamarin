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
    class MyContactAdapter : BaseAdapter
    {

        Context context;
        private List<ContactDataModel> _contacts;
        public MyContactAdapter(Context context, List<ContactDataModel> contact)
        {
            this.context = context;
            _contacts = contact;
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
            MyContactAdapterViewHolder holder = null;

            if (view != null)
                holder = view.Tag as MyContactAdapterViewHolder;

            if (holder == null)
            {
                holder = new MyContactAdapterViewHolder();
                var inflater = context.GetSystemService(Context.LayoutInflaterService).JavaCast<LayoutInflater>();
                //replace with your item and your holder items
                //comment back in
                view = inflater.Inflate(Resource.Layout.ListViewContactCustom, parent, false);
                holder.ContactName= view.FindViewById<TextView>(Resource.Id.tv_contact_name);
                view.Tag = holder;
            }


            //fill in your items
            holder.ContactName.Text = _contacts[position].NAME;

            return view;
        }

        //Fill in cound here, currently 0
        public override int Count
        {
            get
            {
                return _contacts.Count;
            }
        }

    }

    class MyContactAdapterViewHolder : Java.Lang.Object
    {
        //Your adapter views to re-use
        //public TextView Title { get; set; }
        public TextView ContactName { get; set; }
    }
}