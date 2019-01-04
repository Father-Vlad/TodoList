namespace TodoList.Core.Services
{
    public class CurrentUser
    {
        private static string s_userId;

        public static string UserId
        {
            get
            {
                return s_userId;
            }

            set
            {
                s_userId = value;
            }
        }
    }
}