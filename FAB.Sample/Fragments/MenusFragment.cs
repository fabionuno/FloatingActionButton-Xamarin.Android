
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Animation;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Util;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;

using FloatingActionButton = Clans.Fab.FloatingActionButton;
using FloatingActionMenu = Clans.Fab.FloatingActionMenu;
using Fragment = Android.Support.V4.App.Fragment;

namespace FAB.Demo
{
    public class MenusFragment : Fragment, View.IOnClickListener
    {

        private FloatingActionMenu menuRed;
        private FloatingActionMenu menuYellow;
        private FloatingActionMenu menuGreen;
        private FloatingActionMenu menuBlue;
        private FloatingActionMenu menuDown;
        private FloatingActionMenu menuLabelsRight;

        private FloatingActionButton fab1;
        private FloatingActionButton fab2;
        private FloatingActionButton fab3;

        private FloatingActionButton fabEdit;

        private List<FloatingActionMenu> menus = new List<FloatingActionMenu> (6);
        private Handler mUiHandler = new Handler ();


        public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate (Resource.Layout.menus_fragment, container, false);
        }

        public override void OnViewCreated (View view, Bundle savedInstanceState)
        {
            base.OnViewCreated (view, savedInstanceState);

            menuRed = view.FindViewById<FloatingActionMenu> (Resource.Id.menu_red);
            menuYellow = view.FindViewById<FloatingActionMenu> (Resource.Id.menu_yellow);
            menuGreen = view.FindViewById<FloatingActionMenu> (Resource.Id.menu_green);
            menuBlue = view.FindViewById<FloatingActionMenu> (Resource.Id.menu_blue);
            menuDown = view.FindViewById<FloatingActionMenu> (Resource.Id.menu_down);
            menuLabelsRight = view.FindViewById<FloatingActionMenu> (Resource.Id.menu_labels_right);

            fab1 = view.FindViewById<FloatingActionButton> (Resource.Id.fab1);
            fab2 = view.FindViewById<FloatingActionButton> (Resource.Id.fab2);
            fab3 = view.FindViewById<FloatingActionButton> (Resource.Id.fab3);


            FloatingActionButton programFab1 = new FloatingActionButton (this.Activity);
            programFab1.ButtonSize = FloatingActionButton.SizeMini;
            programFab1.LabelText = this.GetString (Resource.String.lorem_ipsum);
            programFab1.SetImageResource (Resource.Drawable.ic_edit);
            menuRed.AddMenuButton (programFab1);
            programFab1.Click += ProgramFab1_Click;

            ContextThemeWrapper context = new ContextThemeWrapper (this.Activity, Resource.Style.MenuButtonsStyle);
            FloatingActionButton programFab2 = new FloatingActionButton (context);
            programFab2.LabelText = "Programmatically added button";
            programFab2.SetImageResource (Resource.Drawable.ic_edit);
            menuYellow.AddMenuButton (programFab2);


            fab1.Enabled = false;
            menuRed.SetOnMenuButtonClickListener (this);
            menuRed.SetClosedOnTouchOutside (true);
            menuBlue.IconAnimated = false;


            menuDown.HideMenuButton (false);
            menuRed.HideMenuButton (false);
            menuYellow.HideMenuButton (false);
            menuGreen.HideMenuButton (false);
            menuBlue.HideMenuButton (false);
            menuLabelsRight.HideMenuButton (false);


            fabEdit = view.FindViewById<FloatingActionButton> (Resource.Id.fab_edit);
            fabEdit.SetShowAnimation (AnimationUtils.LoadAnimation (this.Activity, Resource.Animation.scale_up));
            fabEdit.SetHideAnimation (AnimationUtils.LoadAnimation (this.Activity, Resource.Animation.scale_down));
        }

        public override void OnActivityCreated (Bundle savedInstanceState)
        {
            base.OnActivityCreated (savedInstanceState);

            menus.Add (menuDown);
            menus.Add (menuRed);
            menus.Add (menuYellow);
            menus.Add (menuGreen);
            menus.Add (menuBlue);
            menus.Add (menuLabelsRight);

            menuYellow.MenuToggle += (object sender, FloatingActionMenu.MenuToggleEventArgs e) => 
            {
                string text = (e.Opened ? "Menu opened" : "Menu closed");
                Toast.MakeText (this.Activity, text, ToastLength.Short).Show ();
            };

            fab1.Click += ActionButton_Click;
            fab2.Click += ActionButton_Click;
            fab3.Click += ActionButton_Click;

            int delay = 400;
            foreach (var menu in menus) 
            {
                mUiHandler.PostDelayed (() => menu.ShowMenuButton (true), delay);
                delay += 150;
            }

            new Handler ().PostDelayed (() => fabEdit.Show (true), delay + 150);

            CreateCustomAnimation ();
        }


        private void CreateCustomAnimation ()
        {
            AnimatorSet set = new AnimatorSet ();

            ObjectAnimator scaleOutX = ObjectAnimator.OfFloat (menuGreen.MenuIconView, "scaleX", 1.0f, 0.2f);
            ObjectAnimator scaleOutY = ObjectAnimator.OfFloat (menuGreen.MenuIconView, "scaleY", 1.0f, 0.2f);

            ObjectAnimator scaleInX = ObjectAnimator.OfFloat (menuGreen.MenuIconView, "scaleX", 0.2f, 1.0f);
            ObjectAnimator scaleInY = ObjectAnimator.OfFloat (menuGreen.MenuIconView, "scaleY", 0.2f, 1.0f);

            scaleOutX.SetDuration (50);
            scaleOutY.SetDuration (50);

            scaleInX.SetDuration (150);
            scaleInY.SetDuration (150);

            scaleInX.AnimationStart += (object sender, EventArgs e) => {
                menuGreen.MenuIconView.SetImageResource (menuGreen.IsOpened ? Resource.Drawable.ic_close : Resource.Drawable.ic_star);
            };

            set.Play (scaleOutX).With (scaleOutY);
            set.Play (scaleInX).With (scaleInY).After (scaleOutX);
            set.SetInterpolator (new OvershootInterpolator (2));

            menuGreen.IconToggleAnimatorSet = set;
        }


        private void ActionButton_Click (object sender, EventArgs e)
        {
            FloatingActionButton fabButton = sender as FloatingActionButton;
            if (fabButton != null) 
            {
                if (fabButton.Id == Resource.Id.fab2) 
                {
                    fabButton.Visibility = ViewStates.Gone;
                } 
                else if (fabButton.Id == Resource.Id.fab3) 
                {
                    fabButton.Visibility = ViewStates.Visible;
                }
                Toast.MakeText (this.Activity, fabButton.LabelText, ToastLength.Short).Show ();
            }
        }


        public void OnClick (View v)
        {
            FloatingActionMenu menu = (FloatingActionMenu)v.Parent;
            if (menu.Id == Resource.Id.menu_red && menu.IsOpened) 
            {
                Toast.MakeText (this.Activity, menu.MenuButtonLabelText, ToastLength.Short).Show ();
            }

            menu.Toggle (animate: true);
        }

        private void ProgramFab1_Click (object sender, EventArgs e)
        {
            var fab = sender as FloatingActionButton;

            if (fab != null) 
            {
                fab.SetLabelColors (ContextCompat.GetColor (this.Activity, Resource.Color.grey),
                                    ContextCompat.GetColor (this.Activity, Resource.Color.light_grey),
                                    ContextCompat.GetColor (this.Activity, Resource.Color.white_transparent));
                fab.SetLabelTextColor (ContextCompat.GetColor (this.Activity, Resource.Color.black));
            }
        }
    }
}

