using System;
using System.Collections.Generic;
using System.Text;
using TodoList.Core.Interfaces;
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
        //private 
    }
}
