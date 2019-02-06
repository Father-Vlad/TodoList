using Foundation;
using TodoList.Core.Interfaces;
using TodoList.iOS.Helper;
using UIKit;

namespace TodoList.iOS.Services
{
    public class ShareTextToTelegramService : IShareTextToTelegramService
    {
        private NSUrl _urlShareToTelegram;

        public void ShareText(string message, string toastMessage)
        {
            _urlShareToTelegram = new NSUrl(string.Format("tg://msg?text=" + "Hi, I created a new task for myself with To-do List app.\nThe name of task is: "));
            if (UIApplication.SharedApplication.CanOpenUrl(_urlShareToTelegram))
            {
                UIApplication.SharedApplication.OpenUrl(_urlShareToTelegram);
                return;
            }
            ToastClass.ShowToast(toastMessage);
        }
    }
}