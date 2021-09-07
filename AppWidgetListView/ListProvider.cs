using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Android.Content;
using Android.Widget;
using Java.Util.Prefs;
using Newtonsoft.Json;

namespace AppWidgetListView
{
    public class ListProvider : Java.Lang.Object, RemoteViewsService.IRemoteViewsFactory
    {
        private List<ListItem> listItemList = new List<ListItem>();
        private Context context;
        private int d;
        private int i;
        private int k;
        private DateTime dt;
        private List<CallbackMyWorks> works = new List<CallbackMyWorks>();

        public ListProvider(Context contextNew)

        {
            context = contextNew;
            k = DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month);
            i = 0;
            dt = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            d = ((int)dt.DayOfWeek);
            if (d == 0)
                d = 7;
            works = LoadFromDB(DateTime.Today.Month, DateTime.Today.Year);
            populateListItem();
        }

        public RemoteViews GetViewAt(int position)
        {
            RemoteViews remoteView = new RemoteViews(context.PackageName, Resource.Layout.list_row);
            for (int j = 0; j < 7; j++)
            {
                FillList(remoteView, position, j);
            }
            return remoteView;
        }

        private void FillList(RemoteViews remoteView, int i, int j)
        {
            int res = 0;
            if (j == 0)
                res = Resource.Id.d1;
            else if (j == 1)
                res = Resource.Id.d2;
            else if (j == 2)
                res = Resource.Id.d3;
            else if (j == 3)
                res = Resource.Id.d4;
            else if (j == 4)
                res = Resource.Id.d5;
            else if (j == 5)
                res = Resource.Id.d6;
            else if (j == 6)
                res = Resource.Id.d7;
            remoteView.SetTextViewText(res, listItemList[i].days[j]);
            if ((i == 0 && int.Parse(listItemList[i].days[j]) > 7) || (i > 3 && int.Parse(listItemList[i].days[j]) < 15))
                remoteView.SetInt(res, "setBackgroundColor", Android.Graphics.Color.Gray);
            else
            {
                if (listItemList[i].specs[j].Equals("1"))
                {
                    remoteView.SetInt(res, "setBackgroundColor", Android.Graphics.Color.ParseColor("#bf3737"));
                    remoteView.SetTextColor(res, Android.Graphics.Color.White);
                }
                else if (listItemList[i].specs[j].Equals("2"))
                {
                    remoteView.SetInt(res, "setBackgroundColor", Android.Graphics.Color.ParseColor("#203080"));
                    remoteView.SetTextColor(res, Android.Graphics.Color.White);
                }
                else if (listItemList[i].specs[j].Equals("8") | listItemList[i].specs[j].Equals("7") | listItemList[i].specs[j].Equals("4"))
                {
                    remoteView.SetInt(res, "setBackgroundColor", Android.Graphics.Color.ParseColor("#2f8254"));
                    remoteView.SetTextColor(res, Android.Graphics.Color.White);
                }
                else if (listItemList[i].specs[j].Equals("Î"))
                {
                    remoteView.SetInt(res, "setBackgroundColor", Android.Graphics.Color.ParseColor("#FFE4C4"));
                    remoteView.SetTextColor(res, Android.Graphics.Color.White);
                }
            }

        }

        private void populateListItem()
        {
            ListItem listItem = new ListItem();
            if (d != 1)
            {
                for (int j = 0; j < d - 1; j++)
                {
                    listItem.days[j] = dt.AddDays(-1 * (d - j - 1)).Day.ToString();
                }
            }
            for (int j = d; j <= 7; j++)
            {
                i++;
                DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, i);
                var find = works.FirstOrDefault(x => x.getDate() == dt);
                if (find != null)
                {
                    listItem.specs[j - 1] = find.getType();
                }
                else
                    listItem.specs[j - 1] = "0";
                listItem.days[j - 1] = i.ToString();
            }
            listItemList.Add(listItem);
            for (int z = 0; z <= 4; z++)
            {
                AddFullWeek();
            }

        }

        private void AddFullWeek()
        {
            ListItem listItem = new ListItem();
            for (int j = 0; j < 7; j++)
            {
                i++;
                DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, i);
                var find = works.FirstOrDefault(x => x.getDate() == dt);
                if (find != null)
                {
                    listItem.specs[j] = find.getType();
                }
                else
                    listItem.specs[j] = "0";
                listItem.days[j] = i.ToString();
                if (i == k)
                    i = 0;
            }
            listItemList.Add(listItem);
        }

        public int Count { get { return listItemList.Count; } }


        public long GetItemId(int position)
        {
            return position;
        }


        private int cellNum(int col)
        {
            return 0;
        }

        public RemoteViews LoadingView
        {
            get
            {
                return null;
            }
        }

        public int ViewTypeCount { get { return 1; } }

        public bool HasStableIds { get { return true; } }


        public void OnCreate()
        {
            // throw new NotImplementedException();
        }

        public void OnDataSetChanged()
        {
            // throw new NotImplementedException();
        }

        public void OnDestroy()
        {
            // throw new NotImplementedException();
        }

        private List<CallbackMyWorks> LoadFromDB(int month, int year)
        {
            PreparedCallback pc = new PreparedCallback();
            return JsonConvert.DeserializeObject<List<CallbackMyWorks>>(pc.json);
        }
    }
}