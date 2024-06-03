
namespace TodoApi.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public Boolean IsComplete { get; set; }     
        public int TodoListId { get; set; }
        public TodoList? TodoList { get; set; }
    }
}