using SQLite;

namespace TodoList.Core.Models
{
    [Table("LastUser")]
    public class LastUser
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string UserId { get; set; }

        public LastUser(string userId)
        {
            UserId = userId;
        }

        public LastUser()
        {
        }
    }
}