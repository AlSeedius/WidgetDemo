using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Appwidget;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace AppWidgetListView
{
    [Service(Permission = "android.permission.BIND_REMOTEVIEWS", Exported = false)]
    public class WidgetService : RemoteViewsService
    {
        public override IRemoteViewsFactory OnGetViewFactory(Intent intent)
        {
            ListProvider lp = new ListProvider(this.ApplicationContext);
            return lp;
        }

    }
}