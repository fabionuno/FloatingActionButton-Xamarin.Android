
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using FAB.Demo.Adapter;

using FloatingActionButton = Clans.Fab.FloatingActionButton;
using Fragment = Android.Support.V4.App.Fragment;

namespace FAB.Demo
{
    public class ProgressFragment : Fragment
    {

        private enum ProgressType
        {
            Indeterminate,
            ProgressPositive, ProgressNegative, Hidden, ProgressNoAnimation, ProgressNoBackground
        }

        private int scrollOffset = 4;
        private int maxProgress = 100;
        private LinkedList<ProgressType> progressTypes;
        private Handler uiHandler = new Handler ();
        private FloatingActionButton fab;

        public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate (Resource.Layout.progress_fragment, container, false);
        }

        public override void OnViewCreated (View view, Bundle savedInstanceState)
        {
            base.OnViewCreated (view, savedInstanceState);

            var locales = Java.Util.Locale.GetAvailableLocales ();
            this.progressTypes = new LinkedList<ProgressType> ();
            foreach (var item in (ProgressType [])Enum.GetValues (typeof (ProgressType))) {
                this.progressTypes.AddLast (item);
            }

            fab = view.FindViewById<FloatingActionButton> (Resource.Id.fab);
            fab.Max = this.maxProgress;
            fab.Click += FabClickHandler;


            RecyclerView recyclerView = view.FindViewById<RecyclerView> (Resource.Id.my_recycler_view);
            recyclerView.HasFixedSize = true;
            recyclerView.SetLayoutManager (new LinearLayoutManager (this.Activity));
            recyclerView.SetAdapter (new LanguageAdapter (locales));
            recyclerView.AddOnScrollListener (new RecyclerScrollListener (this.fab, this.scrollOffset));
        }

        private void FabClickHandler (object sender, EventArgs e)
        {
            ProgressType type = this.progressTypes.First ();
            this.progressTypes.RemoveFirst ();
            switch (type) {
            case ProgressType.Indeterminate:
                this.fab.SetShowProgressBackground (true);
                this.fab.SetIndeterminate (true);
                this.progressTypes.AddLast (ProgressType.Indeterminate);
                break;
            case ProgressType.ProgressPositive:
                this.fab.SetIndeterminate (false);
                this.fab.SetProgress (70, true);
                this.progressTypes.AddLast (ProgressType.ProgressPositive);
                break;
            case ProgressType.ProgressNegative:
                this.fab.SetProgress (30, true);
                this.progressTypes.AddLast (ProgressType.ProgressNegative);
                break;
            case ProgressType.Hidden:
                this.fab.HideProgress ();
                this.progressTypes.AddLast (ProgressType.Hidden);
                break;
            case ProgressType.ProgressNoAnimation:
                IncreaseProgress (fab, 0);
                break;
            case ProgressType.ProgressNoBackground:
                this.fab.SetShowProgressBackground (false);
                this.fab.SetIndeterminate (true);
                this.progressTypes.AddLast (ProgressType.ProgressNoBackground);
                break;
            default:
                break;
            }

        }

        private void IncreaseProgress (FloatingActionButton fab, int i)
        {
            if (i <= this.maxProgress) {
                fab.SetProgress (i, false);
                int progress = ++i;
                this.uiHandler.PostDelayed (() => IncreaseProgress (fab, progress), 30);
            } else {
                this.uiHandler.PostDelayed (() => fab.HideProgress (), 200);
                this.progressTypes.AddLast (ProgressType.ProgressNoAnimation);
            }
        }

        private class RecyclerScrollListener : RecyclerView.OnScrollListener
        {
            private readonly FloatingActionButton fab;
            private readonly int scrollOffset;

            public RecyclerScrollListener (FloatingActionButton fab, int scrollOffset)
            {
                this.fab = fab;
                this.scrollOffset = scrollOffset;
            }

            public override void OnScrolled (RecyclerView recyclerView, int dx, int dy)
            {
                base.OnScrolled (recyclerView, dx, dy);

                if (Math.Abs (dy) > this.scrollOffset) {
                    if (dy > 0)
                        this.fab.Hide (true);
                    else
                        this.fab.Show (true);

                }
            }
        }
    }
}

