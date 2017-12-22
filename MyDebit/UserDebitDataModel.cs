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
    public class UserDebitDataModel
    {
        public int ID { get; set; }
        public string NAME { get; set; }
        public int MONEY { get; set; }
        public DateTime DATE { get; set; }
        public bool STATUS { get; set; }
    }

    public class ContactDataModel
    {
        public int ID { get; set; }
        public string NAME { get; set; }
    }
}