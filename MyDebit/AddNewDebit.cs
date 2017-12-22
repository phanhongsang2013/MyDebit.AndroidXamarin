using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Java.Text;
using Org.Apache.Http.Impl;

namespace MyDebit
{
    [Activity(Label = "AddNewDebit", Theme = "@style/MyTheme.Splash")]
    public class AddNewDebit : Activity
    {
        private EditText _txtNumberOfMoney;
        private ListView _lvContact;
        private Button _btnAddContact;
        private Button _btnSave;
        private SangUtil.RestApiRequest _client = new SangUtil.RestApiRequest();
        private List<ContactDataModel> _contacts = new List<ContactDataModel>();
        private string _contactClicked;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AddNewDebit);

            // Create your application here
            InitVariable();
            LoadListContactData();
        }

        private void InitVariable()
        {
            _txtNumberOfMoney = FindViewById<EditText>(Resource.Id.txt_money);

            _lvContact = FindViewById<ListView>(Resource.Id.lv_user);
            _btnAddContact = FindViewById<Button>(Resource.Id.btn_add_contact);
            _btnSave = FindViewById<Button>(Resource.Id.btn_save);

            _lvContact.ItemClick += _lvContact_ItemClick;

            _btnSave.Click += delegate { SaveData(); };
            _btnAddContact.Click += delegate { ShowAddNewContactDialog(); };
        }
        private void _lvContact_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            _contactClicked = _contacts.ElementAt(e.Position).NAME;
        }
        private void LoadListContactData()
        {
            _contacts = _client.GetMethod<ContactDataModel>("MY_DEBIT_CONTACT", null);
            var adapter = new MyContactAdapter(this, _contacts);
            _lvContact.Adapter = adapter;
        }
        private void SaveData()
        {
            int iMoney = Int32.Parse(_txtNumberOfMoney.Text);

            var data = new UserDebitDataModel()
            {
                NAME = _contactClicked,
                STATUS = false,
                DATE = DateTime.Now,
                MONEY = iMoney
            };

            var postResult = _client.PostMethod("MY_DEBIT", data);
            if (postResult.Equals("Created"))
            {
                var intent = new Intent(this, typeof(MainActivity));
                intent.SetFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask);
                StartActivity(intent);
            }
        }
        private void ShowAddNewContactDialog()
        {
            EditText et = new EditText(this);
            AlertDialog.Builder ad = new AlertDialog.Builder(this);
            ad.SetPositiveButton("Save", delegate { AddNewContact(et.Text); });

            ad.SetTitle("Enter your contact's name");
            ad.SetView(et); // <----
            ad.Show();
        }
        private void AddNewContact(string contactName)
        {
            var newContact = new ContactDataModel(){NAME = contactName};

            var postResult = _client.PostMethod("MY_DEBIT_CONTACT", newContact);
            if (postResult.Equals("Created"))
            {
                var intent = new Intent(this, typeof(AddNewDebit));
                intent.SetFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask);
                StartActivity(intent);
            }
        }

    }
}