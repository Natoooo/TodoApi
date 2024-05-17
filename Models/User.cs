using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TodoApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        
        public ICollection<TodoList> TodoLists { get; } = new List<TodoList>();
    }
}