using System.Collections.Generic;
using TodoList.Core.Models;

namespace TodoList.Core.Interfaces
{
    public interface IUserService
    {
        List<User> GetAllUsers();
        User GetUser(string currentUserId);
        void InsertUser(User user);
    }
}