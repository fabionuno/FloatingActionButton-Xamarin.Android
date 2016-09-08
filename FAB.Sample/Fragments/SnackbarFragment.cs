
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;

using FloatingActionButton = Clans.Fab.FloatingActionButton;
using FloatingActionMenu = Clans.Fab.FloatingActionMenu;
using Fragment = Android.Support.V4.App.Fragment;

namespace FAB.Demo
{
    public class SnackbarFragment : Fragment
    {
        private FloatingActionButton fabSend;
        private FloatingActionButton fabDelete;
        private FloatingActionMenu fam;
        private CoordinatorLayout coordinator;

        public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.snackbar_fragment, container, false);
        }

        public override void OnViewCreated (View view, Bundle savedInstanceState)
        {
            base.OnViewCreated (view, savedInstanceState);
            this.fabSend = view.FindViewById<FloatingActionButton> (Resource.Id.fabSend);
            this.fabDelete = view.FindViewById<FloatingActionButton> (Resource.Id.fabDelete);
            this.coordinator = view.FindViewById<CoordinatorLayout> (Resource.Id.coordinatorLayout);

            this.fam = view.FindViewById<FloatingActionMenu> (Resource.Id.menu);
            this.fam.HideMenuButton (false);
        }

        public override void OnActivityCreated (Bundle savedInstanceState)
        {
            base.OnActivityCreated (savedInstanceState);
            this.fam.PostDelayed (() => this.fam.ShowMenuButton (true), 200);
        }

        public override void OnResume ()
        {
            base.OnResume ();
            this.fabSend.Click += FabSend_Click;
            this.fabDelete.Click += FabDelete_Click;
        }

        public override void OnPause ()
        {
            base.OnPause ();
            this.fabSend.Click -= FabSend_Click;
            this.fabDelete.Click -= FabDelete_Click;
        }

        private void FabDelete_Click (object sender, EventArgs e)
        {
            this.fam.Toggle (true);
            Snackbar.Make (this.coordinator, "Email deleted", Snackbar.LengthLong)
                .SetAction ("Undo",
                    (view) => {

                    })
                .Show ();
        }

        private void FabSend_Click (object sender, EventArgs e)
        {
            this.fam.Toggle (true);
            Snackbar.Make (this.coordinator, "Email sent", Snackbar.LengthLong).Show ();
        }
    }
}

