using RentAgent.Models;

namespace RentAgent.Services.Users;

public class UserService:IUserService
{
    private  List<User> _users = new();
    
    public void AddUser(User user)
    {
        _users.Add(user);
    }
    public List<User> GetAllUsers()
    {
        return _users.ToList();
    }
    public User? GetById(int id)
    {
        return _users.FirstOrDefault(u => u.Id == id);
    }
}