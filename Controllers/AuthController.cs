using TodoApi.Managers;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore;


namespace TodoApi.Controllers
{
    [ApiController]
    [Route("auth/")]
    public class TokenController : ControllerBase
    {
        private readonly ApiDbContext _context;
        private readonly TokenManager _tokenManager;

        private readonly UserManager _userManager;

        public TokenController(TokenManager tokenManager, UserManager userManager, ApiDbContext context)
        {
            _context = context;
            _userManager = userManager;
            _tokenManager = tokenManager;
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
        public ActionResult Logout()
        {
            var user = _tokenManager.Authenticate(Request);         
            if (user == null) return  Unauthorized();

            var token = _context.Token.FirstOrDefault(t => t.UserId == user.Id);
            if (token == null) return NotFound();

            _context.Token.Remove(token);
            _context.SaveChanges();

            return Ok();
        }
    }
}