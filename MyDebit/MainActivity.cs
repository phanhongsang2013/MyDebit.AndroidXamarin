using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Widget;
using Android.OS;

namespace MyDebit
{
    [Activity(Theme = "@style/MyTheme.Splash", Label = "My Debit", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

        private ListView _lvDebit;
        private Button _btnAddNew;
        private TextView _tvTotalDebit;

        SangUtil.RestApiRequest _client = new SangUtil.RestApiRequest();
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
             SetContentView (Resource.Layout.Main);

            _lvDebit = FindViewById<ListView>(Resource.Id.listViewDebit);
            _btnAddNew = FindViewById<Button>(Resource.Id.button_add_new);
            _btnAddNew.Click += _btnAddNew_Click;
            _tvTotalDebit = FindViewById<TextView>(Resource.Id.tv_sum_debit);

            LoadDebitData();
            LoadListViewData();
        }

        private void _btnAddNew_Click(object sender, System.EventArgs e)
        {
            ShowAddNewFragment();
        }


        private void ShowAddNewFragment()
        {
            StartActivity(typeof(AddNewDebit));
        }

        private List<UserDebitDataModel> LoadDebitData()
        {
            var dataGet = _client.GetMethod<UserDebitDataModel>("MY_DEBIT", null);
            SetTotalDebit(dataGet.Where(i=>i.STATUS == false).ToList());
            return dataGet;
        }

        private void SetTotalDebit(List<UserDebitDataModel> dataGet)
        {
            int totalDebit = 0;
            foreach (var item in dataGet)
            {
                totalDebit += item.MONEY;
            }

            SangUtil control = new SangUtil();
            var txtTotalDebit = control.ConvertIntToCurrency(totalDebit);
            _tvTotalDebit.Text = txtTotalDebit;
        }

        private void LoadListViewData()
        {
            ListViewAdapter adapter = new ListViewAdapter(this, LoadDebitData());
            _lvDebit.Adapter = adapter;
        }
    }
}

