using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Threading.Tasks;
using TodoList.Core.Interfaces;
using TodoList.Core.ViewModels;

namespace TodoList.Core
{
    public class AppStart : MvxAppStart
    {
        private readonly ILoginService _loginService;
        public AppStart(IMvxApplication app, IMvxNavigationService mvxNavigationService, ILoginService loginService)
            : base(app, mvxNavigationService)
        {
            _loginService = loginService;
        }

        protected override Task NavigateToFirstViewModel(object hint = null)
        {
            return NavigationService.Navigate<LoginViewModel>();
        }
    }
}