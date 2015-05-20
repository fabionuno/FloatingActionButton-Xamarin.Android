using System;
using System.Linq;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Android.Support.V7.AppCompat;
using Android.Support.V7.App;
using Android.Views.Animations;
using Clans.Fab;


namespace FAB.Demo
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private int mPreviousVisibleItem;
        private FloatingActionButton fab;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.main_activity);

            var locales = Java.Util.Locale.GetAvailableLocales().Select(c => c.DisplayName).ToList<String>();
		
            ListView listView = FindViewById<ListView>(Resource.Id.list);
            listView.Adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1,
                    Android.Resource.Id.Text1, locales);

            fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Hide(false);

            new Handler().PostDelayed(ShowFab, 300);

            fab.Click += (sender, e) => StartActivity(new Intent(this, typeof(FloatingMenusActivity)));
            
            listView.Scroll += ListViewScrollHanlder;
        }

        private void ShowFab()
        {
            fab.Show(true);
            fab.SetShowAnimation(AnimationUtils.LoadAnimation(this, Resource.Animation.show_from_bottom));
            fab.SetHideAnimation(AnimationUtils.LoadAnimation(this, Resource.Animation.hide_to_bottom));
        }

        private void ListViewScrollHanlder(object sender, AbsListView.ScrollEventArgs e)
        {
            if (e.FirstVisibleItem > mPreviousVisibleItem)
            {
                fab.Hide(true);
            }
            else if (e.FirstVisibleItem < mPreviousVisibleItem)
            {
                fab.Show(true);
            }

            mPreviousVisibleItem = e.FirstVisibleItem;   
        }
    }
}


