using Newtonsoft.Json.Linq;
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
        private readonly string _serviceIdFacebookLastUser = "FacebookLastUser";
        private OAuth2Authenticator _auth;
        private Account _currentUserAccount;
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
            var data = AccountStore.Create().FindAccountsForService(_serviceIdFacebookLastUser).FirstOrDefault();
            if (data != null)
            {
                AccountStore.Create().Delete(data, _serviceIdFacebookLastUser);
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
                AccountStore.Create().Save(loggedInAccount, _serviceIdFacebookLastUser);
            }
        }

        public Account CurrentUserAccount
        {
            get
            {
                return _currentUserAccount = AccountStore.Create().FindAccountsForService(_serviceIdFacebookLastUser).FirstOrDefault();
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
                    User user = new User(CurrentUserAccount.Username, string.Empty);
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
