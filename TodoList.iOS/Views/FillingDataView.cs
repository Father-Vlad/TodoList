using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using TodoList.Core.ViewModels;
using UIKit;

namespace TodoList.iOS.Views
{
    [MvxModalPresentation(WrapInNavigationController = true, ModalTransitionStyle = UIModalTransitionStyle.CrossDissolve)]
    public partial class FillingDataView : MvxViewController<FillingDataViewModel>
    {
        private UIBarButtonItem _buttonGoBack;
        public FillingDataView() : base(nameof(FillingDataView), null)
        {
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            _buttonGoBack = new UIBarButtonItem(UIBarButtonSystemItem.Reply, null);
            NavigationItem.SetLeftBarButtonItem(_buttonGoBack, false);

            var set = this.CreateBindingSet<FillingDataView, FillingDataViewModel>();
            set.Bind(NameOfTaskTextField).To(vm => vm.GoalName);
            set.Bind(NameOfTaskTextField).For(v => v.Enabled).To(vm => vm.SaveButtonEnableStatus);
            set.Bind(DescriptionOfTaskTextField).To(vm => vm.GoalDescription);
            set.Bind(StatusOfTaskSwitch).To(vm => vm.GoalStatus);
            set.Bind(SaveButton).To(vm => vm.SaveDataCommand);
            set.Bind(DeleteButton).To(vm => vm.DeleteDataCommand);
            set.Bind(_buttonGoBack).For("Clicked").To(vm => vm.SendBackCommand);
            set.Apply();
        }
    }
}