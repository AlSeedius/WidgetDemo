using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Appwidget;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace AppWidgetListView
{
	[BroadcastReceiver (Label = "widget_name")]
	[IntentFilter (new string [] { "android.appwidget.action.APPWIDGET_UPDATE" })]
	[MetaData ("android.appwidget.provider", Resource = "@xml/widgetinfo")]
    public class WidgetProvider : AppWidgetProvider
    {
        public override void OnUpdate(Context context, AppWidgetManager appWidgetManager, int[] appWidgetIds)
        {
            //base.OnUpdate(context, appWidgetManager, appWidgetIds);

            int N = appWidgetIds.Length;

            for (int i = 0; i < N; i++)
            {
                RemoteViews remoteViews = updateWidgetListView(context, appWidgetIds[i]);
                appWidgetManager.UpdateAppWidget(appWidgetIds[i], remoteViews);
            }

        }

        public override void OnReceive(Context context, Intent intent)
        {
            base.OnReceive(context, intent);
        }

        private RemoteViews updateWidgetListView(Context context, int appWidgetId)
        {
            RemoteViews remoteViews = new RemoteViews(context.PackageName, Resource.Layout.widget_layout);
            string PACKAGE_NAME = context.PackageName;


            Intent svcIntent = new Intent(context, typeof(WidgetService));
           svcIntent.SetPackage(PACKAGE_NAME);
           svcIntent.PutExtra(AppWidgetManager.ExtraAppwidgetId, appWidgetId);

           svcIntent.SetData(Android.Net.Uri.Parse(svcIntent.ToUri(IntentUriType.AndroidAppScheme)));


            var intent = new Intent(context, typeof(WidgetProvider));
            intent.SetAction(AppWidgetManager.ActionAppwidgetUpdate);
            intent.PutExtra(AppWidgetManager.ExtraAppwidgetIds, appWidgetId);
            var piBackground = PendingIntent.GetBroadcast(context, 0, intent, PendingIntentFlags.UpdateCurrent);
            remoteViews.SetOnClickPendingIntent(Resource.Id.d1, piBackground);


            remoteViews.SetRemoteAdapter(Resource.Id.listViewWidget, svcIntent);

           

            return remoteViews;
        }
    }
}
