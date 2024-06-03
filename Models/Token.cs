
namespace TodoApi.Models
{
    public class Token
    {
        public int Id { get; set; }
        public string? Content { get; set; }      
        public int UserId { get; set; }       
        public User? User { get; set; }
    }
}