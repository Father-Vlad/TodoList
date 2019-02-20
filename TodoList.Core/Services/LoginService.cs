using Newtonsoft.Json.Linq;
using System;
using System.Net;
using TodoList.Core.Helper;
using TodoList.Core.Interfaces;
using Xamarin.Auth;

namespace TodoList.Core.Services
{
    public class LoginService : ILoginService
    {
        private readonly string _facebookClientId = "1838603119596376";
        private readonly string _facebookAuthorizeUrl = "https://m.facebook.com/dialog/oauth/";
        private readonly string _facebookRedirectUrl = "https://www.facebook.com/connect/login_success.html";
        private readonly string _facebookRequestUrl = "https://graph.facebook.com/me?fields=id,name,picture,email";
        private OAuth2Authenticator _auth;

        public Action OnLoggedInHandler { get; set; }
        public Action OnLoggedOutHandler { get; set; }

        public void LoginFacebook()
        {
            _auth = new OAuth2Authenticator(clientId: _facebookClientId, scope: string.Empty, authorizeUrl: new Uri(_facebookAuthorizeUrl), redirectUrl: new Uri(_facebookRedirectUrl))
            {
                Title = "To-do List",
                AllowCancel = true
                
            };
            _auth.Completed += FacebookAuthCompleted;
            _auth.Error += _auth_Error;
        }

        private void _auth_Error(object sender, AuthenticatorErrorEventArgs e)
        {
            _auth.ShowErrors = false;
            _auth.OnCancelled();
        }

        public void LogoutFacebook()
        {
            if (CurrentUser.IsCurrentUserIdExist())
            {
                CurrentUser.DropCurrentUser();
                OnLoggedOutHandler?.Invoke();
            }
        }

        private async void FacebookAuthCompleted(object sender, AuthenticatorCompletedEventArgs e)
        {
            if (e.IsAuthenticated)
            {
                var request = new OAuth2Request("GET", new Uri(_facebookRequestUrl), null, e.Account);
                var response = await request.GetResponseAsync();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var userJson = response.GetResponseText();
                    var jobject = JObject.Parse(userJson);
                    CurrentUser.SetCurrentUserId(jobject["id"]?.ToString());
                    CurrentUser.SetCurrentUserName(jobject["name"]?.ToString());
                    OnLoggedInHandler?.Invoke();
                }
            }
        }

        public OAuth2Authenticator Authenticator()
        {
            return _auth;
        }
    }
}
