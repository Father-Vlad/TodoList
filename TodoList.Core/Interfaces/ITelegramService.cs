namespace TodoList.Core.Interfaces
{
    public interface ITelegramService
    {
        void ShareText(string shareText);
        bool IsTheAppInstalled(string appName);
        void ShowToastMessage(string toastMessage);
    }
}
