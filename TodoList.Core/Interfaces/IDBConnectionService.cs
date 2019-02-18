using SQLite;

namespace TodoList.Core.Interfaces
{
    public interface IDBConnectionService
    {
        SQLiteConnection GetDataBaseConnection();
    }
}