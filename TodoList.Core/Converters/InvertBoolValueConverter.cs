﻿using MvvmCross.Converters;
using System;
using System.Globalization;

namespace TodoList.Core.Converters
{
    public class InvertBoolValueConverter : MvxValueConverter<bool>
    {
        protected override object Convert(bool value, Type targetType, object parameter, CultureInfo culture)
        {
            return value ? false : true;
        }
    }
}