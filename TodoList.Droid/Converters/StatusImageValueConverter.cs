using MvvmCross.Converters;
using System;
using System.Globalization;

namespace TodoList.Droid.Converters
{
    public class StatusImageValueConverter : MvxValueConverter<bool>
    {
        protected override object Convert(bool value, Type targetType, object parameter, CultureInfo culture)
        {
            return value ? Resource.Drawable.checkbox_checked_50 : Resource.Drawable.checkbox_unchecked_50;
        }
    }
}