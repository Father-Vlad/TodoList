using SQLite;
using System.Collections.Generic;
using System.Linq;
using TodoList.Core.Interfaces;
using TodoList.Core.Models;

namespace TodoList.Core.Services
{
    public class UserService : IUserService
    {
        private SQLiteConnection _sqlConnection;

        public UserService(IDataBaseConnectionService connection)
        {
            _sqlConnection = connection.GetDataBaseConnection();
            _sqlConnection.CreateTable<User>();
        }
        public List<User> GetAllUsers()
        {
            return (from data in _sqlConnection.Table<User>() select data).ToList();
        }

        public User GetUser(string currentUserId)
        {
            return _sqlConnection.Table<User>().FirstOrDefault(x => x.UserId == currentUserId);
        }

        public void InsertUser(User user)
        {
            if (user.Id != 0)
            {
                _sqlConnection.Update(user);
            }
            if (user.Id == 0)
            {
                _sqlConnection.Insert(user);
            }
        }
    }
}