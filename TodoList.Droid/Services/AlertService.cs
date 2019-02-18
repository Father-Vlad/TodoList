using Android.App;
using Android.Widget;
using TodoList.Core.Interfaces;

namespace TodoList.Droid.Services
{
    public class AlertService : IAlertService
    {
        public void ShowToast(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Short).Show();
        }
    }
}