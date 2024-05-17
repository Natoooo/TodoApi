using Microsoft.EntityFrameworkCore;
using TodoApi.Managers;
using TodoApi.Middlewares;
using TodoApi.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        var conn = builder.Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddDbContext<ApiDbContext>(options =>
            options.UseNpgsql(conn));

        builder.Services.AddTransient<ItemManager>();
        builder.Services.AddTransient<TodoListManager>();
        builder.Services.AddTransient<UserManager>();
        builder.Services.AddTransient<TokenManager>();


        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseWhen(context => context.Request.Path != "/auth/login", builder => builder.UseAuthMiddleware() );

        app.MapControllers();

        app.Run();
    }
}