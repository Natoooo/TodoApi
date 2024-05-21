using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using Microsoft.EntityFrameworkCore;
using TodoApi.Middlewares;


namespace TodoApi.Controllers
{
    [ApiController]
    [Route("auth/")]
    public class TokenController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public TokenController(ApiDbContext context)
        {
            _context = context;
        }
        
        
        [HttpPost, Route("Login")]
        public ActionResult Login([FromBody] User userCredentials)
        {
            var user = _context.User.FirstOrDefault(u => u.Email == userCredentials.Email);

            if (user == null || user.Password != userCredentials.Password)
                return Unauthorized();

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
        public ActionResult Logout()
        {
            var user = _context.User.FirstOrDefault();        
            if (user == null)
                return  Unauthorized();

            var token = _context.Token.FirstOrDefault(t => t.UserId == user.Id);
            
            if (token == null)
                return NotFound();

            _context.Token.Remove(token);
            _context.SaveChanges();
            return Ok();
        }
    }
}