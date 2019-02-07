namespace TodoList.Core.Interfaces
{
    public interface IShareTextToTelegramService
    {
        void ShareText(string shareText);
        bool IsTheAppInstalled(string appName);
        void ShowToastMessage(string toastMessage);
    }
}
