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
        private ListView listView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.main_activity);

            var locales = Java.Util.Locale.GetAvailableLocales().Select(c => c.DisplayName).ToList<String>();
		
            this.listView = FindViewById<ListView>(Resource.Id.list);
            this.listView.Adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1,
                Android.Resource.Id.Text1, locales);

            this.fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            this.fab.Hide(false);

            this.fab.PostDelayed(ShowFab, 300);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            this.MenuInflater.Inflate(Resource.Menu.main_menu, menu);
            return true;
        }

        protected override void OnResume()
        {
            base.OnResume();
            this.fab.Click += Fab_Click;
            this.listView.Scroll += ListView_Scroll;
            ;
        }

        protected override void OnPause()
        {
            base.OnPause();
            this.fab.Click -= Fab_Click;
            this.listView.Scroll -= ListView_Scroll;
            ;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menu_snack:
                    ShowSnackActivity();
                    break;
                default:
                    return base.OnOptionsItemSelected(item);
            }

            return base.OnOptionsItemSelected(item);
        }

        private void ShowSnackActivity()
        {
            StartActivity(new Intent(this, typeof(SnackActivity)));
        }

        private void ShowFab()
        {
            fab.Show(true);
            fab.SetShowAnimation(AnimationUtils.LoadAnimation(this, Resource.Animation.show_from_bottom));
            fab.SetHideAnimation(AnimationUtils.LoadAnimation(this, Resource.Animation.hide_to_bottom));
        }

        private void Fab_Click(object sender, EventArgs e)
        {
            StartActivity(new Intent(this, typeof(FloatingMenusActivity)));
        }

        private void ListView_Scroll(object sender, AbsListView.ScrollEventArgs e)
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