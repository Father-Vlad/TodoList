using Foundation;
using MvvmCross.Converters;
using System;
using System.Globalization;
using UIKit;

namespace TodoList.iOS.Converters
{
    public class UrlPictureStringValueConverter : MvxValueConverter<string>
    {
        protected override object Convert(string value, Type targetType, object parameter, CultureInfo culture)
        {
            var _urlPictureString = string.Format("https://graph.facebook.com/" + value + "/picture?type=large");// + "&height=200&width=200";
            return string.IsNullOrEmpty(value) ? UIImage.FromBundle("LaunchScreen") : UIImage.LoadFromData(NSData.FromUrl(new NSUrl(_urlPictureString)));
        }
    }
}