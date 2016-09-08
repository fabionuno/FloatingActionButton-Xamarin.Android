
using Android.App;
using Android.Support.V4.App;

using Fragment = Android.Support.V4.App.Fragment;
using FragmentManager = Android.Support.V4.App.FragmentManager;

namespace FAB.Demo
{
    public class TabsPagerAdapter : FragmentPagerAdapter
    {
        private string[] tabTitles;

        public TabsPagerAdapter(FragmentManager fm) : base(fm)
        {
            this.tabTitles = new string[3] { "CALLS", "CHATS", "CONTACTS" };
        }

        public override int Count
        {
            get
            {
                return this.tabTitles.Length;
            }
        }

        public override Fragment GetItem(int position)
        {
            return new HomeFragment(hideFab: true);
        }

        public override Java.Lang.ICharSequence GetPageTitleFormatted(int position)
        {
            return new Java.Lang.String(this.tabTitles[position]);
        }
    }
}