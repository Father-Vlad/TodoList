using Foundation;
using MvvmCross.Platforms.Ios.Core;
using TodoList.Core;
using UIKit;

namespace TodoList.iOS
{
    [Register(nameof(AppDelegate))]
    public class AppDelegate : MvxApplicationDelegate<Setup, App>
    {
        public override UIWindow Window
        {
            get;
            set;
        }

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            var result = base.FinishedLaunching(application, launchOptions);
            return result;
        }

    }
}


