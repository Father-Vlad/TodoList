using Foundation;
using TodoList.Core.Interfaces;
using UIKit;

namespace TodoList.iOS.Services
{
    public class TelegramService : ITelegramService
    {
        public void ShareText(string shareText)
        {
            UIApplication.SharedApplication.OpenUrl(new NSUrl(shareText));
        }

        public bool IsTheAppInstalled(string appName)
        {
            return UIApplication.SharedApplication.CanOpenUrl(new NSUrl(appName));
        }
    }
}