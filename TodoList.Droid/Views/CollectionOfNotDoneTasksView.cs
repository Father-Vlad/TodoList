using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using TodoList.Core.ViewModels;

namespace TodoList.Droid.Views
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame, false)]
    [Register("TodoList.Droid.Views.CollectionOfNotDoneTasksView")]
    public class CollectionOfNotDoneTasksView : BaseFragment<UncompletedGoalsViewModel>
    {
        #region Variables
        private RecyclerAdapter _recyclerAdapter;
        private RecyclerView.LayoutManager _layoutManager;
        private MvxRecyclerView _recyclerView;
        #endregion Variables

        #region Lifecycle
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);
            _recyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.recycler_view_main);
            _layoutManager = new LinearLayoutManager(Context);
            _recyclerView.SetLayoutManager(_layoutManager);
            _recyclerAdapter = new RecyclerAdapter((IMvxAndroidBindingContext)this.BindingContext);
            _recyclerAdapter.OnTelegramShareClickAdapter = (currentTask) =>
            {
                this.ViewModel.PlatformName = true; //if false -> iOS, if true -> Android
                this.ViewModel.ShareMessageCommand.Execute(currentTask);
            };
            _recyclerView.Adapter = _recyclerAdapter;
            return view;
        }
        #endregion Lifecycle

        #region Overrides
        protected override int FragmentId
        {
            get
            {
                return Resource.Layout.CollectionLayout;
            }
        }
        #endregion Overrides
    }
}