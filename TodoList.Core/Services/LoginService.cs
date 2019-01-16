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
        private readonly string _FacebookClientId = "1838603119596376";
        private readonly string _FacebookAuthorizeUrl = "https://m.facebook.com/dialog/oauth/";
        private readonly string _FacebookRedirectUrl = "https://www.facebook.com/connect/login_success.html";
        private readonly string _FacebookRequestUrl = "https://graph.facebook.com/me?fields=id,name,picture,email";
        private OAuth2Authenticator _auth;
        private Account _currentUserAccount;
        private User _currentUser;

        public Action OnLoggedInHandler { get; set; }
        public LoginService()
        {

        }

        public void LoginFacebook()
        {
            _auth = new OAuth2Authenticator(clientId: _FacebookClientId, scope: string.Empty, authorizeUrl: new Uri(_FacebookAuthorizeUrl), redirectUrl: new Uri(_FacebookRedirectUrl))
            {
                AllowCancel = true
            };
            _auth.Completed += FacebookAuthCompleted;
        }

        public void LogoutFacebook()
        {
            var data = AccountStore.Create().FindAccountsForService("FacebookLastUser").FirstOrDefault();
            if (data != null)
            {
                AccountStore.Create().Delete(data, "FacebookLastUser");
            }
        }

        private async void FacebookAuthCompleted(object sender, AuthenticatorCompletedEventArgs e)
        {
            if (e.IsAuthenticated)
            {
                Account loggedInAccount = e.Account;
                AccountStore.Create().Save(loggedInAccount, "FacebookLastUser");
                var request = new OAuth2Request("GET", new Uri(_FacebookRequestUrl), null, e.Account);
                var response = await request.GetResponseAsync();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var userJson = response.GetResponseText();
                    var jobject = JObject.Parse(userJson);
                    var userId = jobject["id"]?.ToString();
                    var userName = jobject["name"]?.ToString();
                    CurrentUser = new User(userId, userName);
                    OnLoggedInHandler();
                }
            }
        }

        public Account CurrentUserAccount
        {
            get
            {
                return _currentUserAccount = AccountStore.Create().FindAccountsForService("FacebookLastUser").FirstOrDefault();
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
                return _currentUser;
            }

            private set
            {
                _currentUser = value;
            }
        }
    }
}
