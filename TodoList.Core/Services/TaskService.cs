using SQLite;
using System.Collections.Generic;
using System.Linq;
using TodoList.Core.Interfaces;

namespace TodoList.Core.Services
{
    public class TaskService : ITaskService
    {
        private SQLiteConnection _sqlConnection;

        public TaskService(IDataBaseConnectionService connection)
        {
            _sqlConnection = connection.GetDataBaseConnection();
            _sqlConnection.CreateTable<Goal>();
        }

        public List<Goal> GetAllGoals()
        {
            return (from data in _sqlConnection.Table<Goal>() select data).ToList();
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

        //public Goal GetGoalData(int goalId)
        //{
        //    return _sqlConnection.Table<Goal>().FirstOrDefault(x => x.Id == goalId);
        //}

        //public void UpdateGoal(Goal goal)
        //{
        //    _sqlConnection.Update(goal);
        //}

        //public void DeleteAllGoals()
        //{
        //    _sqlConnection.DeleteAll<Goal>();
        //}
    }
}