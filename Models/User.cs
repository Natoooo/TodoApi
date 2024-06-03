
namespace TodoApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public ICollection<TodoList> TodoLists { get; } = new List<TodoList>();
    }
}