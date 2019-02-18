using SQLite;
using System.IO;
using TodoList.Core.Interfaces;

namespace TodoList.Droid.Services
{
    public class DBConnectionService : IDBConnectionService
    {
        public DBConnectionService()
        {
            var connection = GetDataBaseConnection();
        }
        public SQLiteConnection GetDataBaseConnection()
        {
            var dbName = "ToDoList.db";
            var path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), dbName);
            return new SQLiteConnection(path);
        }
    }
}   