namespace TodoList.Core.Interfaces
{
    public interface IShareTextToTelegramService
    {
        void ShareText(string message, string toastMessage);
    }
}
