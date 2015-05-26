using System;
using Android.Support.V7.Widget;
using Android.Widget;
using Android.Views;


namespace FAB.Demo.Adapter
{
    public class LanguageAdapter : RecyclerView.Adapter
    {

        private Java.Util.Locale[] locales;

        public LanguageAdapter(Java.Util.Locale[] locales) 
        {
            this.locales = locales;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(Android.Views.ViewGroup parent, int viewType)
        {
            TextView tv = (TextView)LayoutInflater.From(parent.Context).
                Inflate(Android.Resource.Layout.SimpleListItem1, parent, false);

            return new ViewHolder(tv);
        }
    
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            ((ViewHolder)holder).textView.Text = this.locales[position].DisplayName;
       }

        public override int ItemCount
        {
            get
            {
                return this.locales.Length;
            }
        }


        private class ViewHolder : RecyclerView.ViewHolder 
        {
            public TextView textView;

            public ViewHolder(TextView v) : base(v)
            {
                this.textView = v;
            }
        }
    }
}

