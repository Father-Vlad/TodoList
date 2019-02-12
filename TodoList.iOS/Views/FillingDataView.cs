using CoreGraphics;
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
        private UIButton _buttonGoBack;
        private UIColor _placeholderDescriptionColor;
        private readonly string _textTitle = "Write TODO sample...";
        private readonly string _namePlaceholder = "Enter your Goal Name";
        private readonly string _descriptionPlaceholder = "Enter your Description";

        public FillingDataView() : base(nameof(FillingDataView), null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Title = _textTitle;
            _buttonGoBack = new UIButton(UIButtonType.Custom);
            _buttonGoBack.Frame = new CGRect(0, 0, 40, 40);
            _buttonGoBack.SetImage(UIImage.FromBundle("FillingDataBackIcon"), UIControlState.Normal);
            this.NavigationItem.SetLeftBarButtonItem(new UIBarButtonItem(_buttonGoBack), false);
            UINavigationBar.Appearance.SetTitleTextAttributes(new UITextAttributes() { TextColor = UIColor.White });
            NavigationController.NavigationBar.BarTintColor = new UIColor(0.17f, 0.24f, 0.31f, 1.0f);
            _placeholderDescriptionColor = new UIColor(0.78f, 0.78f, 0.8f, 1.0f);
            //Binding
            var set = this.CreateBindingSet<FillingDataView, FillingDataViewModel>();
            set.Bind(NameOfTaskTextField).To(vm => vm.GoalName);
            set.Bind(NameOfTaskTextField).For(v=>v.Enabled).To(vm => vm.GoalNameEnableStatus);
            set.Bind(DescriptionOfTaskTextView).To(vm => vm.GoalDescription);
            set.Bind(StatusOfTaskSwitch).To(vm => vm.GoalStatus);
            set.Bind(SaveButton).To(vm => vm.SaveDataCommand);
            set.Bind(SaveButton).For(v => v.Enabled).To(vm => vm.SaveButtonEnableStatus);
            set.Bind(DeleteButton).To(vm => vm.DeleteDataCommand);
            set.Bind(_buttonGoBack).To(vm => vm.SendBackCommand);
            set.Bind(StatusOfTaskLabel).To(vm => vm.GoalStatus).WithConversion("StatusOfTaskLabel");
            set.Bind(DeleteButton).For("Title").To(vm => vm.DeleteCanselButtonText); 
            set.Apply();
            if (DescriptionOfTaskTextView.Text == _descriptionPlaceholder || string.IsNullOrEmpty(DescriptionOfTaskTextView.Text))
            {
                DescriptionOfTaskTextView.Text = _descriptionPlaceholder;
                DescriptionOfTaskTextView.TextColor = _placeholderDescriptionColor;
            }
            //The string that is displayed when there is no other text in the text field.
            NameOfTaskTextField.Placeholder = _namePlaceholder;
            DescriptionOfTaskTextView.ShouldBeginEditing = textView =>
            {
                if (textView.Text == _descriptionPlaceholder)
                {
                    textView.Text = string.Empty;
                    textView.TextColor = UIColor.Black;
                }
                return true;
            };
            DescriptionOfTaskTextView.ShouldEndEditing = textView =>
            {
                if (string.IsNullOrEmpty(textView.Text))
                {
                    textView.Text = _descriptionPlaceholder;
                    textView.TextColor = _placeholderDescriptionColor;
                }
                return true;
            };
            //Hide the keyboard after using it for entering data in a text field
            NameOfTaskTextField.ShouldReturn = (textField) => {
                textField.ResignFirstResponder();
                return true;
            };
            var endEditing = new UITapGestureRecognizer(() => View.EndEditing(true));
            endEditing.CancelsTouchesInView = false;
            View.AddGestureRecognizer(endEditing);
            //Buttons Border Width & Color
            SaveButton.Layer.BorderWidth = 1;
            SaveButton.Layer.BorderColor = UIColor.Black.CGColor;
            DeleteButton.Layer.BorderWidth = 1;
            DeleteButton.Layer.BorderColor = UIColor.Black.CGColor;
        }
    }
}