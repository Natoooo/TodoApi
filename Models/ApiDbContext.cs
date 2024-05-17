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
            // modelBuilder.Entity<User>()
            //     .HasMany(e => e.TodoLists)
            //     .WithOne(e => e.User)
            //     .HasForeignKey(e => e.UserId)
            //     .IsRequired(false);

            // modelBuilder.Entity<TodoList>()
            //     .HasMany(e => e.Items)
            //     .WithOne(e => e.TodoList)
            //     .HasForeignKey(e => e.TodoListId)
            //     .IsRequired(false);
        }
    }
}
