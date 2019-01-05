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
        //Goal GetGoalData(int goalId);
        //void UpdateGoal(Goal goal);
        //void DeleteAllGoals();

        //For Users
        List<User> GetAllUsers();
        User GetUser(string currentUserId);
        void InsertUser(User user);

        //For LastUser
        string GetLastUser();
        void InsertOrReplaceLastUser(LastUser user);
    }
}