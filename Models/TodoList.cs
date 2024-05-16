using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TodoApi.Models
{
    public class TodoList
    {
        public int Id { get; set; }
        public string? Name { get; set; }
       
        //public int UserId { get; set; }       
        //public User? User { get; set; }
        public ICollection<Item> Items { get; } = new List<Item>();
    }
}