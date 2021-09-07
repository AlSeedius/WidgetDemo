using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppWidgetListView
{
    public class CallbackMyWorks
    {
        private string type;
        private DateTime date;
        public CallbackMyWorks(String type, DateTime date)
        {
            this.type = type;
            this.date = date;
        }

        public String getType()
        {
            return type;
        }

        public void setType(String type)
        {
            this.type = type;
        }

        public DateTime getDate()
        {
            return date;
        }

        public void setDate(DateTime date)
        {
            this.date = date;
        }
    }
}