using SQLite;

namespace TodoList.Core.Models
{
    [Table("Goal")]
    public class Goal
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string GoalName { get; set; }
        public string GoalDescription { get; set; }
        public bool GoalStatus { get; set; }

        public Goal(int id, string goalName, string goalDescription, bool goalStatus)
        {
            Id = id;
            GoalName = goalName;
            GoalDescription = goalDescription;
            GoalStatus = goalStatus;
        }
        public Goal()
        {

        }
    }
}