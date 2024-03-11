using Microsoft.AspNetCore.Mvc;
using WebApplication1.Constants;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers;

public class UserController : Controller
{
    UserRepository _repository = UserRepository.GetRepository();

    public IActionResult LogIn()
    {
        return View();
    }

    public IActionResult LogOut()
    {
        CookieOptions options = new CookieOptions();
        Response.Cookies.Delete(UserConstants.CookieEmail);
        return Redirect("/Home/Index");
    }

    [HttpPost]
    public IActionResult Validate(IFormCollection collection)
    {
        collection.TryGetValue(UserConstants.FormEmailField, out var email);
        collection.TryGetValue(UserConstants.FormPasswordField, out var password);
        collection.TryGetValue(UserConstants.FormRememberMeField, out var rememberMe);
        if (!ModelState.IsValid) return View("LogIn");

        if (_repository.GetUserByEmail(email) == null)
        {
            ViewData["error"] = "no such user";
            return View("LogIn");
        }

        if (!_repository.GetUserByEmail(email).Password.Equals(password))
        {
            ViewData["error"] = "wrong password";
            return View("LogIn");
        }

        CookieOptions options = new CookieOptions();

        if (rememberMe.Equals("on"))
        {
            options.MaxAge = TimeSpan.FromDays(30);
        }

        Response.Cookies.Append(UserConstants.CookieEmail, email, options);
        return Redirect("/Home/Index");
    }

    public IActionResult IsAdmin(string email)
    {
        return Json(_repository.IsAdmin(email));
    }
}