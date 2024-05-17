using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace TodoApi.Models
{
    public class Token
    {
        public int Id { get; set; }
        public string? Content { get; set; }
       
        public int UserId { get; set; }       
        [JsonIgnore]
        public User? User { get; set; }
    }
}