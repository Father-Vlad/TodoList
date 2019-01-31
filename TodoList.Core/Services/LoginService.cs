using Newtonsoft.Json.Linq;
using Plugin.Settings;
using System;
using System.Linq;
using System.Net;
using TodoList.Core.Interfaces;
using TodoList.Core.Models;
using Xamarin.Auth;

namespace TodoList.Core.Services
{
    public class LoginService : ILoginService
    {
        private readonly string _facebookClientId = "1838603119596376";
        private readonly string _facebookAuthorizeUrl = "https://m.facebook.com/dialog/oauth/";
        private readonly string _facebookRedirectUrl = "https://www.facebook.com/connect/login_success.html";
        private readonly string _facebookRequestUrl = "https://graph.facebook.com/me?fields=id,name,picture,email";
        private readonly string _keyForSettingId = "LastUserId";
        private readonly string _keyForSettingName = "LastUserName";
        private OAuth2Authenticator _auth;
        private string _currentUserId;
        private string _currentUserName;
        private User _currentUser;

        public Action OnLoggedInHandler { get; set; }
        public Action OnLoggedOutHandler { get; set; }

        public void LoginFacebook()
        {
            _auth = new OAuth2Authenticator(clientId: _facebookClientId, scope: string.Empty, authorizeUrl: new Uri(_facebookAuthorizeUrl), redirectUrl: new Uri(_facebookRedirectUrl))
            {
                AllowCancel = true
            };
            _auth.Completed += FacebookAuthCompleted;
        }

        public void LogoutFacebook()
        {
            var data = CrossSettings.Current.GetValueOrDefault(_keyForSettingId, string.Empty).ToString(); AccountStore.Create().FindAccountsForService(_keyForSettingId).FirstOrDefault();
            if (CrossSettings.Current.Contains(_keyForSettingId, string.Empty))
            {
                CrossSettings.Current.Clear();
                //AccountStore.Create().Delete(data, _keyForSettingId);
                if (OnLoggedOutHandler != null)
                {
                    OnLoggedOutHandler();
                }
            }
        }

        private async void FacebookAuthCompleted(object sender, AuthenticatorCompletedEventArgs e)
        {
            if (e.IsAuthenticated)
            {
                Account loggedInAccount = e.Account;
                var request = new OAuth2Request("GET", new Uri(_facebookRequestUrl), null, e.Account);
                var response = await request.GetResponseAsync();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var userJson = response.GetResponseText();
                    var jobject = JObject.Parse(userJson);
                    var userId = jobject["id"]?.ToString();
                    var userName = jobject["name"]?.ToString();
                    loggedInAccount.Username = userId;
                    CurrentUser = new User(userId, userName);
                    OnLoggedInHandler();
                }
                CrossSettings.Current.AddOrUpdateValue(_keyForSettingId, CurrentUser.UserId);
                CrossSettings.Current.AddOrUpdateValue(_keyForSettingName, CurrentUser.UserName);
                //AccountStore.Create().Save(loggedInAccount, _keyForSettingId);
            }
        }

        public string CurrentUserId
        {
            get
            {
                return _currentUserId = CrossSettings.Current.GetValueOrDefault(_keyForSettingId, string.Empty).ToString();
            }
        }

        public string CurrentUserName
        {
            get
            {
                return _currentUserName = CrossSettings.Current.GetValueOrDefault(_keyForSettingName, string.Empty).ToString();
            }
        }

        public OAuth2Authenticator Authenticator()
        {
            return _auth;
        }
        
        public User CurrentUser
        {
            get
            {
                if (_currentUser == null)
                {
                    User user = new User(CurrentUserId, CurrentUserName); //This is the right version of the user id parameter
                    //User user = new User("1", string.Empty); //This is the test version
                    _currentUser = user;
                }
                return _currentUser;
            }

            set
            {
                _currentUser = value;
            }
        }
    }
}
