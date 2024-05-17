using TodoApi.Managers;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;


namespace TodoApi.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly ApiDbContext _context;
        private readonly UserManager _userManager;

        public UserController(UserManager userManager, ApiDbContext context)
        {
            _context = context;
            _userManager = userManager;
            _userManager.InitContext();
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {

            Console.WriteLine("Getting all users");
            return Ok(_userManager.GetAllUsers());
        }

        [HttpPost]
        public ActionResult<User> SignUp(User user)
        {
            Console.WriteLine("Create a new sign up");
            var newUser = _userManager.CreateUser(user);
            return Ok(newUser);
        }

        [HttpPut("{id}")]
        public ActionResult<User> PutUser(int id, User user)
        {
            Console.WriteLine("Update user");
            var updatedUser = _userManager.UpdateUser(id, user);
            return Ok(updatedUser);
        }
    }
}