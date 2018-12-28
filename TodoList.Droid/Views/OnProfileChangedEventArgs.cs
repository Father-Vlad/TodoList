using System;
using Xamarin.Facebook;

namespace TodoList.Droid.Views
{
    public class OnProfileChangedEventArgs : EventArgs
    {
        public Profile mProfile;
        public OnProfileChangedEventArgs(Profile profile)
        {
            mProfile = profile;
        }
        //Extract or delete HTML tags based on their name or whether or not they contain some attributes or content with the HTML editor pro online program.
    }
}