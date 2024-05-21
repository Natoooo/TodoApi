using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace TodoApi.Models
{
    public class TodoList
    {
        public int Id { get; set; }
        public string? Name { get; set; }
       
        public int UserId { get; set; }       
        [JsonIgnore]
        public User? User { get; set; }
        [JsonIgnore]
        public ICollection<Item> Items { get; } = new List<Item>();
    }
}