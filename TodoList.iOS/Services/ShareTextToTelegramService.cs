using Foundation;
using TodoList.Core.Interfaces;
using TodoList.iOS.Helper;
using UIKit;

namespace TodoList.iOS.Services
{
    public class ShareTextToTelegramService : IShareTextToTelegramService
    {
        public void ShareText(string shareText)
        {
            try
            {
                UIApplication.SharedApplication.OpenUrl(new NSUrl(shareText));
            }
            catch
            {
                ShowToastMessage("Something went wrong");
            }
            
        }

        public bool IsTheAppInstalled(string appName)
        {
            return UIApplication.SharedApplication.CanOpenUrl(new NSUrl(appName));
        }

        public void ShowToastMessage(string toastMessage)
        {
            ToastClass.ShowToast(toastMessage);
        }
    }
}