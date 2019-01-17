using System.Collections.Generic;
using TodoList.Core.Models;

namespace TodoList.Core.Interfaces
{
    public interface ITaskService
    {
        //For Goals
        List<Goal> GetAllGoals();
        void InsertGoal(Goal goal);
        void DeleteGoal(int goalId);
        List<Goal> GetUserGoal(string currentUserId);
        List<Goal> GetDoneUserGoal(string currentUserId);
        List<Goal> GetNotDoneUserGoal(string currentUserId);

        //For Users
        List<User> GetAllUsers();
        User GetUser(string currentUserId);
        void InsertUser(User user);
    }
}