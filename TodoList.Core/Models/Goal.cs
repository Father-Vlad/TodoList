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
        public string UserId { get; set; }

        public Goal(int id, string goalName, string goalDescription, bool goalStatus, string userId)
        {
            Id = id;
            GoalName = goalName;
            GoalDescription = goalDescription;
            GoalStatus = goalStatus;
            UserId = userId;
        }
        public Goal()
        {
        }
    }
}