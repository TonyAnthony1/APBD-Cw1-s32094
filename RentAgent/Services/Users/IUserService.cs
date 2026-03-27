using RentAgent.Models;

namespace RentAgent.Services.Users;

public interface IUserService
{
    List<User> GetAllUsers();
    User? GetById(int id);
    void AddUser(User user);
}