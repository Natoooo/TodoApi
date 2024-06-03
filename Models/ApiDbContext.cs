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
        public DbSet<Token> Token {get;set;}


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Token>()
                .HasIndex(t => t.Content)
                .IsUnique();
        }
    }
}
