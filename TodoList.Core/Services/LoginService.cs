using TodoList.Core.Interfaces;

namespace TodoList.Core.Services
{
    public class LoginService : ILoginService
    {
        private bool _isAuthenticated = false;
        public bool IsAuthenticated
        {
            get
            {
                return _isAuthenticated;
            }

            set
            {
                _isAuthenticated = value;
            }
        }
    }
}