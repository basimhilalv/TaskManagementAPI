using TaskManagementAPI.Models;

namespace TaskManagementAPI.Services
{
    public interface IUserServices
    {
        User? RegisterUser(UserDto user);
        string? LoginUser(UserDto user);

        string loginuser(String username, String password);
        IEnumerable<User> GetUsers();
    }
}
