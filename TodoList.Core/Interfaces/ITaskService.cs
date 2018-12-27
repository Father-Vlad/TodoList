using System.Collections.Generic;
using TodoList.Core.Services;

namespace TodoList.Core.Interfaces
{
    public interface ITaskService
    {
        List<Goal> GetAllGoals();
        void InsertGoal(Goal goal);
        void DeleteGoal(int goalId);
        //Goal GetGoalData(int goalId);
        //void UpdateGoal(Goal goal);
        //void DeleteAllGoals();
    }
}