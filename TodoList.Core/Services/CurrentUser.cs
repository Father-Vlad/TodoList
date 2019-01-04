namespace TodoList.Core.Services
{
    public class CurrentUser
    {
        private static string _currentUserId = string.Empty;

        public static string CurrentUserId
        {
            get
            {
                return _currentUserId;
            }

            set
            {
                _currentUserId = value;
            }
        }
    }
}