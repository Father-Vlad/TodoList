using Foundation;
using TodoList.Core.Interfaces;
using UIKit;

namespace TodoList.iOS.Services
{
    public class AlertService : IAlertService
    {
        public void ShowToast(string message)
        {
            UIApplication.SharedApplication.InvokeOnMainThread(() =>
            {
                UIAlertView alert = new UIAlertView()
                {
                    Message = message,
                    Alpha = 1.0f
                };

                alert.Frame = new CoreGraphics.CGRect(alert.Frame.X, UIScreen.MainScreen.Bounds.Y, alert.Frame.Width, alert.Frame.Height);
                NSTimer tmr;
                alert.Show();

                tmr = NSTimer.CreateTimer(2, delegate
                {
                    alert.DismissWithClickedButtonIndex(0, true);
                    alert = null;
                });

                NSRunLoop.Main.AddTimer(tmr, NSRunLoopMode.Common);
            });
        }
    }
}