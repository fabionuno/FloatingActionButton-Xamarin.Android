
using System;
using System.Linq;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Views.Animations;

using Fragment = Android.Support.V4.App.Fragment;
using FloatingActionButton = Clans.Fab.FloatingActionButton;


namespace FAB.Demo
{
    public class HomeFragment : Fragment
    {
        private FloatingActionButton fab;
        private int previousVisibleItem;
        private ListView listView;
        private readonly bool hideFab;


        public HomeFragment(bool hideFab = false)
        {
            this.hideFab = hideFab;
        }

       
        public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate (Resource.Layout.home_fragment, container, false);
        }

        public override void OnViewCreated (View view, Bundle savedInstanceState)
        {
            base.OnViewCreated (view, savedInstanceState);
            this.listView = view.FindViewById<ListView> (Resource.Id.list);
            this.fab = view.FindViewById<FloatingActionButton> (Resource.Id.fab);

        }

        public override void OnActivityCreated (Bundle savedInstanceState)
        {
            base.OnActivityCreated (savedInstanceState);
            var locales = Java.Util.Locale.GetAvailableLocales ().Select (c => c.DisplayName).ToList<String> ();

            this.listView.Adapter = new ArrayAdapter (this.Activity, Android.Resource.Layout.SimpleListItem1,
                Android.Resource.Id.Text1, locales);


            this.fab.Hide (false);
            if (!this.hideFab)
                this.fab.PostDelayed (ShowFab, 300);
        }

        public override void OnResume ()
        {
            base.OnResume ();
            this.listView.Scroll += ListView_Scroll;
            ;
        }

        public override void OnPause ()
        {
            base.OnPause ();
            this.listView.Scroll -= ListView_Scroll;
            ;
        }

        private void ShowFab ()
        {
            this.fab.Show (true);
            this.fab.SetShowAnimation (AnimationUtils.LoadAnimation (this.Activity, Resource.Animation.show_from_bottom));
            this.fab.SetHideAnimation (AnimationUtils.LoadAnimation (this.Activity, Resource.Animation.hide_to_bottom));
        }

        private void ListView_Scroll (object sender, AbsListView.ScrollEventArgs e)
        {
            if (this.hideFab)
                return;

            if (e.FirstVisibleItem > previousVisibleItem)
            {
                fab.Hide(true);
            }
            else if (e.FirstVisibleItem < previousVisibleItem)
            {
                fab.Show(true);
            }

            previousVisibleItem = e.FirstVisibleItem;
        }
    }
}