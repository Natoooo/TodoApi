using Microsoft.EntityFrameworkCore;


namespace TodoApi.Models
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
            
        }
        public DbSet<Item> Item {get;set;}
        public DbSet<TodoList> TodoList {get;set;}
        public DbSet<User> User {get;set;}
    }
}
