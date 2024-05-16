using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApi.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public Boolean isComplete { get; set; }
       
        public int TodoListId { get; set; }
        [ForeignKey("TodoListId")]
        public TodoList? TodoList { get; set; }
    }
}