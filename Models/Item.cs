using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace TodoApi.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public Boolean IsComplete { get; set; }
       
        public int TodoListId { get; set; }
        [JsonIgnore]
        public TodoList? TodoList { get; set; }
    }
}