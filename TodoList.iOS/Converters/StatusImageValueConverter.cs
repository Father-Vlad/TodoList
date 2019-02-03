using MvvmCross.Converters;
using System;
using System.Globalization;
using UIKit;

namespace TodoList.iOS.Converters
{
    public class StatusImageValueConverter : MvxValueConverter<bool>
    {
        protected override object Convert(bool value, Type targetType, object parameter, CultureInfo culture)
        {
            return value ? UIImage.FromBundle("StatusTaskIconChecked") : UIImage.FromBundle("StatusTaskIconUnChecked");
        }
    }
}