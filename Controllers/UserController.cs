using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using TodoApi.Middlewares;
using TodoApi.ViewModels;
using Microsoft.AspNetCore.Identity;


namespace TodoApi.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly ApiDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly ILogger<UserController> _logger;

        public UserController(ApiDbContext context, IPasswordHasher<User> passwordHasher, ILogger<UserController> logger)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _logger = logger;
        }


        [HttpPost]
        public ActionResult<User>? SignUp(UserRegistrationViewModel userRegistration)
        {   
            if (userRegistration.Password == null)
            {
                _logger.LogDebug($"Password is null for email {userRegistration.Email}");
                return Forbid();
            }

            User? user = new User
            {
                Email = userRegistration.Email
            };
            user.PasswordHash = _passwordHasher.HashPassword(user, userRegistration.Password);

            _context.User.Add(user);
            _context.SaveChanges();
            return Ok(user);
        }

        [HttpPut("{id}")]
        [AuthMiddleware]
        public ActionResult<User>? PutUser(int id, [FromBody] User modifiedUser)
        {
            User? currentUser = HttpContext.Items["User"] as User;

            if (currentUser == null || currentUser.Id != id)
            {
                _logger.LogDebug($"Unauthorized access attempt by user ID {currentUser?.Id}");
                return Forbid();
            }

            var user = _context.User.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                _logger.LogDebug($"User with ID {id} not found");
                return Forbid();
            }

            user.Email = modifiedUser.Email;
            _context.SaveChanges();
            return Ok(user);
        }
    }
}