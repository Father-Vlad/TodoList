using SQLite;

namespace TodoList.Core.Models
{
    [Table("User")]
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }

        public User(string userId, string userFirstName, string userLastName)
        {
            UserId = userId;
            UserFirstName = userFirstName;
            UserLastName = userLastName;
        }
        public User()
        {
        }
    }
}