using System.Collections.Generic;
using TodoList.Core.Models;

namespace TodoList.Core.Interfaces
{
    public interface ITaskService
    {
        List<Goal> GetAllGoals();
        void InsertGoal(Goal goal);
        void DeleteGoal(int goalId);
        List<Goal> GetUserGoal(string currentUserId);
        //Goal GetGoalData(int goalId);
        //void UpdateGoal(Goal goal);
        //void DeleteAllGoals();
        List<User> GetAllUsers();
        void InsertUser(User user);
    }
}