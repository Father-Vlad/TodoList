using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Widget;
using TodoList.Core.Interfaces;

namespace TodoList.Droid.Services
{
    class TelegramService : ITelegramService
    {
        private readonly string _dataMIMEType = "text/plain";
        private readonly string _titleOfChooserIntent = "Share with";
        private string _appName = string.Empty;
        private Intent _actionSendIntent;
        private Intent _chooserIntent;

        public bool IsTheAppInstalled(string appName)
        {
            _appName = appName;
            try
            {
                Application.Context.PackageManager.GetPackageInfo(appName, PackageInfoFlags.Activities);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void ShareText(string shareText)
        {
            _actionSendIntent = new Intent(Intent.ActionSend);
            _actionSendIntent.SetType(_dataMIMEType);
            _actionSendIntent.SetPackage(_appName);
            if (_actionSendIntent != null)
            {
                _actionSendIntent.PutExtra(Intent.ExtraText,shareText);
                _chooserIntent = Intent.CreateChooser(_actionSendIntent, _titleOfChooserIntent);
                Application.Context.StartActivity(_chooserIntent);
            }
        }

        public void ShowToastMessage(string toastMessage)
        {
            Toast.MakeText(Application.Context, toastMessage, ToastLength.Short).Show();
        }
    }
}