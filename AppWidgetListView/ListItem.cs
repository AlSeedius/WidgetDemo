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

namespace AppWidgetListView
{
    public class ListItem
    {
        public string[] days = new string[7];
        public string[] specs = new string[7];

        public ListItem()
        {

        }
    }
}