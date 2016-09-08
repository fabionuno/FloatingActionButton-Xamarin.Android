
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Util;
using Android.Views;
using Android.Widget;

using FloatingActionButton = Clans.Fab.FloatingActionButton;
using FloatingActionMenu = Clans.Fab.FloatingActionMenu;
using Fragment = Android.Support.V4.App.Fragment;

namespace FAB.Demo
{
    public class TabFragment : Fragment
    {
        private ViewPager viewPager;
        private TabLayout tabLayout;

        private FloatingActionMenu menuRed;

        public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.tabs_fragment, container, false);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
            this.viewPager = view.FindViewById<ViewPager>(Resource.Id.viewpager);
            this.tabLayout = view.FindViewById<TabLayout>(Resource.Id.tabs);
            this.menuRed = view.FindViewById<FloatingActionMenu>(Resource.Id.menu_red);
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            SetupViewPager(this.viewPager);
            SetupTabLayout(this.viewPager);
        }

        public override void OnResume()
        {
            base.OnResume();
            this.viewPager.PageSelected += ViewPager_PageSelected;
        }

        public override void OnPause()
        {
            base.OnPause();
            this.viewPager.PageSelected -= ViewPager_PageSelected;
        }


        private void SetupTabLayout(ViewPager viewPager)
        {
            this.tabLayout.TabGravity = TabLayout.GravityFill;
            this.tabLayout.SetupWithViewPager(viewPager);
        }

        private void SetupViewPager(ViewPager viewPager)
        {
            TabsPagerAdapter adapter = new TabsPagerAdapter(this.ChildFragmentManager);
            this.viewPager.Adapter = adapter;
        }

        private void ViewPager_PageSelected(object sender, ViewPager.PageSelectedEventArgs e)
        {
            if (this.menuRed.IsOpened)
                this.menuRed.Close(false);
        }
    }
}

