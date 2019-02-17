using MvvmCross.Converters;
using System;
using System.Globalization;

namespace TodoList.iOS.Converters
{
    public class NetValueConverter : MvxValueConverter<bool>
    {
        protected override object Convert(bool value, Type targetType, object parameter, CultureInfo culture)
        {
            return value ? false : true;
        }
    }
}