using Android.App;
using Android.OS;
using Android.Support.V7.Widget;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using TodoList.Core.ViewModels;

namespace TodoList.Droid.Views
{
    [Activity]
    public class MainActivityView : MvxAppCompatActivity<CollectionViewModel>
    {
        private RecyclerAdapter _recyclerAdapter;
        private RecyclerView.LayoutManager _layoutManager;
        private MvxRecyclerView _recyclerView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.MainActivityViewLayout);
            _recyclerView = FindViewById<MvxRecyclerView>(Resource.Id.recycler_view_main);
            _layoutManager = new LinearLayoutManager(this);
            _recyclerView.SetLayoutManager(_layoutManager);
            _recyclerAdapter = new RecyclerAdapter((IMvxAndroidBindingContext)this.BindingContext);
            _recyclerAdapter.ItemClick += _recyclerAdapter_ItemClick;
            _recyclerView.Adapter = _recyclerAdapter;

        }

        private void _recyclerAdapter_ItemClick(object sender, int e)
        {
            this.ViewModel.NextActivityCommand.Execute(this.ViewModel.Goals[e]);
        }
    }
}