using System;
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

            //Hide the keyboard after using it for entering data in a text field
            NameOfTaskTextField.ShouldReturn = (textField) => TextFieldResignFirstResponder(textField);
            DescriptionOfTaskTextField.ShouldReturn = (textField) => TextFieldResignFirstResponder(textField);

            //Remove on tapping the view
            var endEditing = new UITapGestureRecognizer(() => View.EndEditing(true));
            endEditing.CancelsTouchesInView = false;
            View.AddGestureRecognizer(endEditing);

            var set = this.CreateBindingSet<FillingDataView, FillingDataViewModel>();
            set.Bind(NameOfTaskTextField).To(vm => vm.GoalName);
            set.Bind(DescriptionOfTaskTextField).To(vm => vm.GoalDescription);
            set.Bind(StatusOfTaskSwitch).To(vm => vm.GoalStatus);
            set.Bind(SaveButton).To(vm => vm.SaveDataCommand);
            set.Bind(SaveButton).For(v => v.Enabled).To(vm => vm.SaveButtonEnableStatus);
            set.Bind(DeleteButton).To(vm => vm.DeleteDataCommand);
            set.Bind(_buttonGoBack).For("Clicked").To(vm => vm.SendBackCommand);
            set.Apply();
        }

        private bool TextFieldResignFirstResponder(UITextField textField)
        {
            textField.ResignFirstResponder();
            return true;
        }
    }
}