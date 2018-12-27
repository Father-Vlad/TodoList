using SQLite;

namespace TodoList.Core.Interfaces
{
    public interface IDataBaseConnectionService
    {
        SQLiteConnection GetDataBaseConnection();
    }
}