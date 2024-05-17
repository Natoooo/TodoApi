using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Middlewares 
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ApiDbContext _context;

        public AuthMiddleware(RequestDelegate next, ApiDbContext context)
        {
            _next = next;
            _context = context;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var tokenString = context.Request.Headers["X-Authenticate"].FirstOrDefault();
            if (!string.IsNullOrEmpty(tokenString))
            {
                var token = await _context.Token.Include(t => t.User).FirstAsync(t => t.Content == tokenString);

                if (token != null)
                {
                    context.Items["User"] = token.User;
                }
            }

            await _next(context);
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