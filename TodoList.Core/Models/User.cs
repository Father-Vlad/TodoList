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
        public string UserLastName { get; set; }

        public User(int id, string userId, string userName, string userLastName)
        {
            Id = id;
            UserId = userId;
            UserName = userName;
            UserLastName = userLastName;
        }
        public User()
        {
        }
    }
}