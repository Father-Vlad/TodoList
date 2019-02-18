using SQLite;
using System.Collections.Generic;
using System.Linq;
using TodoList.Core.Interfaces;
using TodoList.Core.Models;

namespace TodoList.Core.Services
{
    public class GoalService : IGoalService
    {
        private SQLiteConnection _sqlConnection;

        public GoalService(IDBConnectionService connection)
        {
            _sqlConnection = connection.GetDataBaseConnection();
            _sqlConnection.CreateTable<Goal>();
        }

        public List<Goal> GetAllGoals()
        {
            return (from data in _sqlConnection.Table<Goal>() select data).ToList();
        }

        public List<Goal> GetUserGoal(string currentUserId)
        {
            return (from data in _sqlConnection.Table<Goal>() where data.UserId == currentUserId select data).ToList();
        }

        public void InsertGoal(Goal goal)
        {
            if (goal.Id != 0)
            {
                _sqlConnection.Update(goal);
            }

            if (goal.Id == 0)
            {
                _sqlConnection.Insert(goal);
            }
        }

        public void DeleteGoal(int goalId)
        {
            _sqlConnection.Delete<Goal>(goalId);
        }

        public Goal GetCurrentGoal(int goalId)
        {
            return (from data in _sqlConnection.Table<Goal>() where data.Id == goalId select data).FirstOrDefault();
        }

        public List<Goal> GetDoneUserGoal(string currentUserId)
        {
            return (from data in _sqlConnection.Table<Goal>() where data.UserId == currentUserId where data.GoalStatus == true select data).ToList();
        }

        public List<Goal> GetNotDoneUserGoal(string currentUserId)
        {
            return (from data in _sqlConnection.Table<Goal>() where data.UserId == currentUserId where data.GoalStatus == false select data).ToList();
        }

        public void InsertAllUserGoals(List<Goal> goals)
        {
            _sqlConnection.InsertAll(goals);
        }

        public void DeleteAllUserGoals(string user)
        {
            _sqlConnection.Table<Goal>().Where(x => x.UserId == user).Delete();
        }
    }
}