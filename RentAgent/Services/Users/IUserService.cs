using RentAgent.Models;

namespace RentAgent.Services.Users;

public interface IUserService
{
    List<User> GetAllUsers();
    User? FindUser(int id);
    void AddUser(User user);
}