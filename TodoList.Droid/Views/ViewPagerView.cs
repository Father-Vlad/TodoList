using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.Transitions;
using Android.Support.V4.View;
using Android.Views;
using Java.Lang;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using System.Collections.Generic;
using TodoList.Core.ViewModels;
using TodoList.Droid.Adapter;

namespace TodoList.Droid.Views
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame, false)]
    [Register("TodoList.Droid.Views.ViewPagerView")]
    public class ViewPagerView : BaseFragment<ViewPagerViewModel>
    {
        private ViewPager _viewPager;
        private TabLayout _tabLayout;

        protected override int FragmentId
        {
            get
            {
                return Resource.Layout.ViewPagerLayout;
            }
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);
            _viewPager = view.FindViewById<ViewPager>(Resource.Id.view_pager_main);
            _tabLayout = view.FindViewById<TabLayout>(Resource.Id.tab_layout_main);
            var fragments = new List<MvxFragmentStatePagerAdapter.FragmentInfo>
            {
                new MvxFragmentStatePagerAdapter.FragmentInfo
                {
                    FragmentType = typeof(CollectionOfDoneTasksView),
                    Title = "Done",
                    ViewModel = ViewModel.CompletedGoalsViewModel
                },

                new MvxFragmentStatePagerAdapter.FragmentInfo
                {
                    FragmentType = typeof(CollectionOfNotDoneTasksView),
                    Title = "Not Done",
                    ViewModel = ViewModel.UncompletedGoalsViewModel
                }
            };
            _viewPager.Adapter = new MvxFragmentStatePagerAdapter(Activity, ChildFragmentManager, fragments);
            _tabLayout.SetupWithViewPager(_viewPager);
            _tabLayout.GetTabAt(0).SetIcon(Resource.Drawable.checkbox_checked_20);
            _tabLayout.GetTabAt(1).SetIcon(Resource.Drawable.checkbox_unchecked_20);
            return view;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Activity.OverridePendingTransition(Android.Resource.Animation.FadeIn, Android.Resource.Animation.FadeOut);
        }
    }
}