using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementAPI.Models;
using TaskManagementAPI.Services;

namespace TaskManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly IUserServices _userServices;
        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }
        [HttpPost("register")]
        public ActionResult<User> RegisterUser(UserDto user)
        {
            var newUser = _userServices.RegisterUser(user);
            if (newUser == null)
            {
                return BadRequest("User already exists");
            }
            return Ok(newUser);
        }
        [HttpPost("login")]
        public ActionResult<User> LoginUser(UserDto user)
        {
            var userToLogin = _userServices.LoginUser(user);
            if (userToLogin == null)
            {
                return BadRequest("Invalid username or password");
            }
            return Ok(userToLogin);
        }
    }
}
