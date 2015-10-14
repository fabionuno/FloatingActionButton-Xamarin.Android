using System;
using Android.Support.Design.Widget;
using Android.Content;
using Android.Util;
using Clans.Fab;
using Android.Views;
using Android.Support.V4.View;
using Android.Runtime;

namespace FAB.Demo
{
    [Register("FAB.Demo.FloatingActionMenuBehavior")]
    public class FloatingActionMenuBehavior : CoordinatorLayout.Behavior
    {
        private float mTranslationY;

        public FloatingActionMenuBehavior(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public override bool LayoutDependsOn(CoordinatorLayout parent, Java.Lang.Object child, Android.Views.View dependency)
        {
            return IsInstanceOf<Snackbar.SnackbarLayout>(dependency);
        }

        public override bool OnDependentViewChanged(CoordinatorLayout parent, Java.Lang.Object child, Android.Views.View dependency)
        {
            if (IsInstanceOf<FloatingActionMenu>(child) && IsInstanceOf<Snackbar.SnackbarLayout>(dependency)) 
            {
                this.UpdateTranslation(parent, (View)child, dependency);
            }

            return false;
        }

        public override void OnDependentViewRemoved(CoordinatorLayout parent, Java.Lang.Object child, View dependency)
        {
            if (IsInstanceOf<FloatingActionMenu>(child) && IsInstanceOf<Snackbar.SnackbarLayout>(dependency)) 
            {
                this.UpdateTranslation(parent, (View)child, dependency);
            }
        }

        private void UpdateTranslation(CoordinatorLayout parent, View child, View dependency) 
        {
            float translationY = this.GetTranslationY(parent, child);
            if (translationY != this.mTranslationY) 
            {
                ViewCompat.Animate(child).Cancel();

                if (Math.Abs(translationY - this.mTranslationY) == (float) dependency.Height)
                {
                    ViewCompat.Animate(child)
                        .TranslationY(translationY)
                        .SetListener((IViewPropertyAnimatorListener) null);
                } 
                else 
                {
                    ViewCompat.SetTranslationY(child, translationY);
                }

                this.mTranslationY = translationY;
            }
        }

        private float GetTranslationY(CoordinatorLayout parent, View child) 
        {
            float minOffset = 0.0F;
            var dependencies = parent.GetDependencies(child);
            int i = 0;

            for (int z = dependencies.Count; i < z; ++i) 
            {
                View view = (View)dependencies[i];
                if (IsInstanceOf<Snackbar.SnackbarLayout>(view) && parent.DoViewsOverlap(child, view)) 
                {
                    minOffset = Math.Min(minOffset, ViewCompat.GetTranslationY(view) - (float) view.Height);
                }
            }

            return minOffset;
        }
    
        private bool IsInstanceOf<T>(object instance)
        {
            return instance.GetType().IsAssignableFrom(typeof(T));
        }
    }
}