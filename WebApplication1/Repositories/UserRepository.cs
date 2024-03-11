using WebApplication1.Constants;
using WebApplication1.Models;

namespace WebApplication1.Repositories;

public class UserRepository
{
    private static UserRepository _repository;
    private static readonly List<User> _users;

    static UserRepository()
    {
        _repository = new UserRepository();
        _users = new List<User>();
        
        _users.Add(new User("admin@gmail", "admin", Roles.Admin));
        _users.Add(new User("user@gmail", "user", Roles.User));
    }

    public static UserRepository GetRepository()
    {
        return _repository;
    }

    public bool IsAdmin(string email)
    {
        return _users.Find(user => user.Email.Equals(email)).Role == Roles.Admin;
    }

    public User GetUserByEmail(string email)
    {
        return _users.Find(user => user.Email.Equals(email));
    }
}