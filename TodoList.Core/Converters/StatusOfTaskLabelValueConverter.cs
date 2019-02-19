using MvvmCross.Converters;
using System;
using System.Globalization;

namespace TodoList.Core.Converters
{
    public class StatusOfTaskLabelValueConverter : MvxValueConverter<bool>
    {
        private readonly string _doneTask = "Done";
        private readonly string _notDoneTask = "Not Done";
        protected override object Convert(bool value, Type targetType, object parameter, CultureInfo culture)
        {
            return value ? _doneTask : _notDoneTask;
        }
    }
}
