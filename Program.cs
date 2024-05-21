using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers().AddNewtonsoftJson(options =>
        {
            //options.JsonSerializerOptions.MaxDepth = 128; // You can increase the maximum depth if necessary
            //options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            //options.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
            //options.JsonSerializerOptions.IgnoreReadOnlyProperties = true;
        });
        
        var conn = builder.Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddDbContext<ApiDbContext>(options =>
            options.UseNpgsql(conn));

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

        //app.UseWhen(context => context.Request.Path != "/auth/login", builder => builder.UseAuthMiddleware() );

        app.MapControllers();

        app.Run();
    }
}