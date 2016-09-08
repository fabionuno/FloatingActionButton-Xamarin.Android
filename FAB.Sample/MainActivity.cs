using System;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Support.V7.AppCompat;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;

using Toolbar = Android.Support.V7.Widget.Toolbar;
using FloatingActionButton = Clans.Fab.FloatingActionButton;
using Fragment = Android.Support.V4.App.Fragment;
using FragmentTransaction = Android.Support.V4.App.FragmentTransaction;

namespace FAB.Demo
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private DrawerLayout drawerLayout;
        private ActionBarDrawerToggle toggle;
        private NavigationView navigationView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.main_activity);

            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            this.toggle = new ActionBarDrawerToggle(this, drawerLayout, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            drawerLayout.AddDrawerListener(this.toggle);

            this.navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);

            if (savedInstanceState == null)
            {
                SupportFragmentManager.BeginTransaction().Add(Resource.Id.fragment, new HomeFragment()).Commit();
            }

            navigationView.SetCheckedItem(Resource.Id.home);
        }

        protected override void OnPostCreate(Bundle savedInstanceState)
        {
            base.OnPostCreate(savedInstanceState);
            this.toggle.SyncState();
        }

        protected override void OnResume()
        {
            base.OnResume();
            this.navigationView.NavigationItemSelected += NavigationView_NavigationItemSelected;
        }

        protected override void OnPause()
        {
            base.OnPause();
            this.navigationView.NavigationItemSelected -= NavigationView_NavigationItemSelected;
        }

        public override void OnBackPressed()
        {
            if (drawerLayout != null && drawerLayout.IsDrawerOpen((int)GravityFlags.Start))
            {
                drawerLayout.CloseDrawer((int)GravityFlags.Start);
            }
            else {
                base.OnBackPressed();
            }
        }

        private void NavigationView_NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        {
            this.drawerLayout.CloseDrawer((int)GravityFlags.Start);
            Fragment fragment = null;
            FragmentTransaction ft = SupportFragmentManager.BeginTransaction();

            switch (e.MenuItem.ItemId)
            {
                case Resource.Id.home:
                    fragment = new HomeFragment();
                    break;
                case Resource.Id.menus:
                    fragment = new MenusFragment();
                    break;
                case Resource.Id.progress:
                    fragment = new ProgressFragment();
                    break;
                case Resource.Id.menu_snack:
                    fragment = new SnackbarFragment();
                    break;
                case Resource.Id.menu_tab:
                    fragment = new TabFragment();
                    break;
            }

            ft.Replace(Resource.Id.fragment, fragment).Commit();
            e.Handled = true;
        }
    }
}