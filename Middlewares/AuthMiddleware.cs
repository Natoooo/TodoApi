using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;


namespace TodoApi.Middlewares 
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext requestContext, ApiDbContext dbContext)
        {
            var tokenString = requestContext.Request.Headers["Authorization"].FirstOrDefault<string>();

            Console.WriteLine(tokenString); 

            var token = await dbContext.Token.Include(t => t.User).FirstOrDefaultAsync(t => t.Content == tokenString);

            if (token == null) 
            {
                requestContext.Response.StatusCode = 401;
                return;
            }

            requestContext.Items["User"] = token.User;
            await _next(requestContext);
        }
    }

    public static class AuthMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthMiddleware>();
        }
    }
}


/*           if (!HttpContext.Items.ContainsKey("User"))
                return Unauthorized();

            var user = HttpContext.Items["User"] as User;
            newTodo.UserId = user.Id;*/