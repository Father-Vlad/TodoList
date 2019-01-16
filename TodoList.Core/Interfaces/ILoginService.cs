using System;
using Xamarin.Auth;

namespace TodoList.Core.Interfaces
{
    public interface ILoginService
    {
        Action OnLoggedInHandler { get; set; }
        void LoginFacebook();
        void LogoutFacebook();
        Account CurrentUserAccount { get;}
        OAuth2Authenticator Authenticator();
    }
}