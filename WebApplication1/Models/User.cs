using WebApplication1.Constants;

namespace WebApplication1.Models;

public class User(string email, string password, Roles role)
{
    public string Email { get; set; } = email;

    public string Password { get; set; } = password;

    public Roles Role { get; set; } = role;
}