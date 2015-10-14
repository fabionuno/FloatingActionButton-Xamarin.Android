
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;
using Android.Support.Design.Widget;
using FloatingActionButton = Clans.Fab.FloatingActionButton;
using Clans.Fab;

namespace FAB.Demo
{
    [Activity(Label = "SnackActivity")]			
    public class SnackActivity : AppCompatActivity
    {
        private FloatingActionButton fabSend;
        private FloatingActionButton fabDelete;
        private FloatingActionMenu fam;
        private CoordinatorLayout coordinator;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.activity_snackbar);
            this.fabSend = FindViewById<FloatingActionButton>(Resource.Id.fabSend);
            this.fabDelete = FindViewById<FloatingActionButton>(Resource.Id.fabDelete);
            this.coordinator = FindViewById<CoordinatorLayout>(Resource.Id.coordinatorLayout);

            this.fam = FindViewById<FloatingActionMenu>(Resource.Id.menu);
            this.fam.HideMenuButton(false);
            this.fam.PostDelayed(() => this.fam.ShowMenuButton(true), 200);
        }

        protected override void OnResume()
        {
            base.OnResume();
            this.fabSend.Click += FabSend_Click; 
            this.fabDelete.Click += FabDelete_Click;
        }

        protected override void OnPause()
        {
            base.OnPause();
            this.fabSend.Click -= FabSend_Click; 
            this.fabDelete.Click -= FabDelete_Click;
        }

        private void FabDelete_Click (object sender, EventArgs e)
        {
            this.fam.Toggle(true);
            Snackbar.Make(this.coordinator, "Email deleted", Snackbar.LengthLong)
                .SetAction("Undo",
                    (view)=>
                    {

                    })
                .Show();
        }

        private void FabSend_Click (object sender, EventArgs e)
        {
            this.fam.Toggle(true);
            Snackbar.Make(this.coordinator, "Email sent", Snackbar.LengthLong).Show();
        }
    }
}