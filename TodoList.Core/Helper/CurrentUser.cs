using Plugin.Settings;

namespace TodoList.Core.Helper
{
    public class CurrentUser
    {
        private static readonly string _keyForSettingId = "LastUserId";
        private static readonly string _keyForSettingName = "LastUserName";

        public static string GetCurrentUserId()
        {
            return CrossSettings.Current.GetValueOrDefault(_keyForSettingId, string.Empty).ToString();
        }

        public static string GetCurrentUserName()
        {
            return CrossSettings.Current.GetValueOrDefault(_keyForSettingName, string.Empty).ToString();
        }

        public static bool IsCurrentUserIdExist()
        {
            return CrossSettings.Current.Contains(_keyForSettingId);
        }

        public static bool IsCurrentUserNameExist()
        {
            return CrossSettings.Current.Contains(_keyForSettingId);
        }

        public static void SetCurrentUserId(string id)
        {
            CrossSettings.Current.AddOrUpdateValue(_keyForSettingId, id);
        }

        public static void SetCurrentUserName(string name)
        {
            CrossSettings.Current.AddOrUpdateValue(_keyForSettingName, name);
        }

        public static void DropCurrentUser()
        {
            CrossSettings.Current.Clear();
        }
    }
}
