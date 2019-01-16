using System;
using TodoList.Core.Models;
using Xamarin.Auth;

namespace TodoList.Core.Interfaces
{
    public interface ILoginService
    {
        Action OnLoggedInHandler { get; set; }
        Action OnLoggedOutHandler { get; set; }
        void LoginFacebook();
        void LogoutFacebook();
        Account CurrentUserAccount { get;}
        OAuth2Authenticator Authenticator();
        User CurrentUser { get; set; }
    }
}