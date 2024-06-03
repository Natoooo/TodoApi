using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;


namespace TodoApi.Middlewares
{
    public class AuthMiddlewareAttribute : TypeFilterAttribute
    {
        public AuthMiddlewareAttribute() : base(typeof(AuthMiddlewareFilter))
        {
        }

        private class AuthMiddlewareFilter : IAsyncActionFilter
        {
            private readonly ApiDbContext _dbContext;

            public AuthMiddlewareFilter(ApiDbContext dbContext)
            {
                _dbContext = dbContext;
            }


            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                var requestContext = context.HttpContext;
                var tokenString = requestContext.Request.Headers["Authorization"].FirstOrDefault();

                Console.WriteLine(tokenString);

                Token? token = await _dbContext.Token.Include(t => t.User).FirstOrDefaultAsync(t => t.Content == tokenString);

                if (token == null)
                {
                    requestContext.Response.StatusCode = 401;
                    return;
                }

                requestContext.Items["User"] = token.User;
                await next();
            }
        }
    }
}