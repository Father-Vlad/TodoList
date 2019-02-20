using System;
using Xamarin.Auth;

namespace TodoList.Core.Interfaces
{
    public interface ILoginService
    {
        Action OnLoggedInHandler { get; set; }
        Action OnLoggedOutHandler { get; set; }
        void LoginFacebook();
        void LogoutFacebook();
        OAuth2Authenticator Authenticator();
    }
}