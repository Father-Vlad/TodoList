using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Views;
using Android.Widget;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using TodoList.Core.Models;

namespace TodoList.Droid.Views
{
    public class RecyclerHolder : MvxRecyclerViewHolder
    {
        public TextView GoalNameHolder { get; set; }
        public CheckBox GoalStatusHolder { get; set; }
        public ImageButton TelegramShare { get; set; }
        private string _shareText = "Hi, I created a new task for myself with To-do List app.\nThe name of task is: ";
        private readonly string _goalDone = "\nIt’s done :)";
        private readonly string _goalNotDone = "\nIt’s not done yet :(";
        private readonly string _dataMIMEType = "text/plain";
        private readonly string _titleOfChooserIntent = "Share with";
        private readonly string _appName = "org.telegram.messenger";
        private Intent _actionSendIntent;
        private Intent _chooserIntent;

        public RecyclerHolder(View itemView, IMvxAndroidBindingContext context) : base(itemView, context)
        {
            GoalNameHolder = itemView.FindViewById<TextView>(Resource.Id.text_view);
            GoalStatusHolder = itemView.FindViewById<CheckBox>(Resource.Id.check_box);
            TelegramShare = itemView.FindViewById<ImageButton>(Resource.Id.image_button_share);
            TelegramShare.Click += ShareEmailToTelegram;
            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<RecyclerHolder, Goal>();
                set.Bind(this.GoalNameHolder).To(x => x.GoalName);
                set.Bind(this.GoalStatusHolder).To(y => y.GoalStatus);
                set.Apply();
            });
        }

        private void ShareEmailToTelegram(object sender, EventArgs e)
        {
            if (IsAppInstalled(_appName))
            {
                _shareText += GoalNameHolder.Text;
                if (GoalStatusHolder.Checked)
                {
                    _shareText += _goalDone;
                }
                if (!GoalStatusHolder.Checked)
                {
                    _shareText += _goalNotDone;
                }
                _actionSendIntent = new Intent(Intent.ActionSend);
                _actionSendIntent.SetType(_dataMIMEType);
                _actionSendIntent.SetPackage(_appName);
                if (_actionSendIntent != null)
                {
                    var uri = "testappforlinks://my_code_is_here";
                    _actionSendIntent.PutExtra(Intent.ExtraText, uri);
                    _chooserIntent = Intent.CreateChooser(_actionSendIntent, _titleOfChooserIntent);
                    Application.Context.StartActivity(_chooserIntent);
                    return;
                }
            }
            Toast.MakeText(Application.Context, "Telegram is not installed", ToastLength.Short).Show();
        }

        private bool IsAppInstalled(string appName)
        {
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
    }
}