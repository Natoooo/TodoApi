using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace TodoApi.Models
{
    public class User
    {
        public int Id { get; set; }
        //[DataType(DataType.EmailAddress)]
        //[Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }
        //[Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
        [JsonIgnore]
        public ICollection<TodoList> TodoLists { get; } = new List<TodoList>();
    }
}