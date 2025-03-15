using TaskManagementAPI.Models;

namespace TaskManagementAPI.Services
{
    public interface IUserServices
    {
        User? RegisterUser(UserDto user);
        string? LoginUser(UserDto user);
        IEnumerable<User> GetUsers();
    }
}
