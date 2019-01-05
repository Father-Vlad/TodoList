using SQLite;

namespace TodoList.Core.Models
{
    [Table("User")]
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }

        public User(string userId, string userName)
        {
            UserId = userId;
            UserName = userName;
        }
        public User()
        {
        }
    }
}