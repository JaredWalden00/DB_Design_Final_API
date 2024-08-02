using System.ComponentModel.DataAnnotations;

namespace DB_Design_Final_API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public int Password { get; set; }
    }
}
