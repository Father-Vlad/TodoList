using System.Collections.Generic;
using TodoList.Core.Models;

namespace TodoList.Core.Interfaces
{
    public interface ITaskService
    {
        List<Goal> GetAllGoals();
        void InsertGoal(Goal goal);
        void DeleteGoal(int goalId);
        Goal GetCurrentGoal(int goalId);
        List<Goal> GetUserGoal(string currentUserId);
        List<Goal> GetDoneUserGoal(string currentUserId);
        List<Goal> GetNotDoneUserGoal(string currentUserId);
        void InsertAllUserGoals(List<Goal> goals);
        void DeleteAllUserGoals(string user);
    }
}