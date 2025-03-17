using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Services
{
    public class UserServices : IUserServices
    {
        public readonly List<User> Users = new List<User>() { new User { Id = Guid.NewGuid(), username = "Basim", password="Basim", role="Admin" },
        new User { Id = Guid.NewGuid(), username = "Basim", password="Basim", role="Admin" }};


        public readonly IConfiguration _configuration;
        public UserServices(IConfiguration configuration)
        {

            _configuration = configuration;
        }

        public string loginuser(String username, String password)
        {
            if(Users.Any(u=>u.username == username && u.password == password))
            {
                return "Login Successfull";
            }
            else
            {
                return "Login Unsuccessfull";
            }
        }
        public User? RegisterUser(UserDto user)
        {
            if (Users.Any(u=> u.username == user.username))
            {
                return null;
            }
            var newUser = new User
            {
                Id = Guid.NewGuid(),
                username = user.username,
                password = user.password,
                role = user.role
            };
            Users.Add(newUser);

            return newUser;
        }

        public IEnumerable<User> GetUsers()
        {  
            return Users;
        }

        public string? LoginUser(UserDto user)
        {
            var userToLogin = Users.FirstOrDefault(u => u.username == user.username && u.password == user.password);
            if (userToLogin == null) { return null; }
            var token = createToken(userToLogin);
            return token;
        }

        private string createToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("AppSettings:Key"));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.username),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.role)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
