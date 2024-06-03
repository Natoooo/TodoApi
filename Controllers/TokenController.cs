using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using TodoApi.Middlewares;
using TodoApi.ViewModels;
using Microsoft.AspNetCore.Identity;


namespace TodoApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class TokenController : ControllerBase
    {
        private readonly ApiDbContext _context;
        private readonly ILogger<TokenController> _logger;
        private readonly IPasswordHasher<User> _passwordHasher;

        public TokenController(ApiDbContext context, ILogger<TokenController> logger, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _logger = logger;
            _passwordHasher = passwordHasher;
        }
        
        
        [HttpPost, Route("Login")]
        public ActionResult Login([FromBody] UserLoginViewModel userCredentials)
        {
            User? user = _context.User.FirstOrDefault(u => u.Email == userCredentials.Email);

            if (user == null)
            {
                _logger.LogDebug("User is null in Login");
                return Forbid();
            }

            if (user.PasswordHash == null)
            {
                _logger.LogDebug($"Unauthorized login attempt for email {userCredentials.Email}: password hash is null or empty");
                return Forbid();
            }

            if (userCredentials.Password == null)
            {
                _logger.LogDebug($"Unauthorized login attempt for email {userCredentials.Email}: password hash is null or empty");
                return Forbid();
            }

            PasswordVerificationResult result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, userCredentials.Password);
            
            if (result == PasswordVerificationResult.Failed)
            {
                _logger.LogDebug($"Unauthorized login attempt for email {userCredentials.Email}: invalid password");
                return Forbid();
            }

            var token = new Token
            {
                Content =  Guid.NewGuid().ToString().ToString(),
                User = user
            };

            _context.Token.Add(token);
            _context.SaveChanges();   
            return Ok(new { token = token.Content });
        }

        [HttpDelete, Route("Logout")]
        [AuthMiddleware]
        public IActionResult Logout()
        {
            User? user = HttpContext.Items["User"] as User;

            if (user == null)
            {
                _logger.LogDebug("User is null in Logout");
                return Forbid();
            }

            Token? token = _context.Token.FirstOrDefault(t => t.UserId == user.Id);

            if (token == null)
            {
                _logger.LogDebug($"Token not found for user ID {user.Id}");
                return Forbid();
            }

            _context.Token.Remove(token);
            _context.SaveChanges();
            return NoContent();
        }
    }
}