using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApi.Models
{
    public class TodoList
    {
        public int Id { get; set; }
        public string? Name { get; set; }
       
        public int UserId { get; set; }       
        [ForeignKey("UserId")]
        public User? User { get; set; }
        public ICollection<Item>? Items { get; set; }
    }
}