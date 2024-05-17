using Microsoft.EntityFrameworkCore;
using TodoApi.Models;


namespace TodoApi.Managers
{
    public class TokenManager
    {
        private readonly ApiDbContext _context;

        public TokenManager (ApiDbContext context)
        {
            _context = context;
        }


        public User? Authenticate(HttpRequest request)
        {
            var tokenString = request.Headers["X-Authenticate"].FirstOrDefault();
            if (tokenString == null) return null;

            Token token = _context.Token.Include(t => t.User).First(t => t.Content == tokenString);

            return token.User;
        }
    }
}    