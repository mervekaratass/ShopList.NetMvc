using EntityLayer.Entities;
using EntityLayer.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingList.ViewModels;

namespace ShoppingList.Controllers
{
    public class RegisterController : Controller
    {
        ShoppingDbContext context;
        public RegisterController() {
        context = new ShoppingDbContext();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel p)
        {
            if (ModelState.IsValid)
            {
                var login = context.Users.FirstOrDefault(x => x.Email == p.Email);

                if (login == null)
                {

                    User user = new User();
                    user.UserName = p.UserName;
                    user.UserSurname = p.UserSurname;
                    user.Email = p.Email;
                    user.Password = p.Password;
                    user.ConfirmPassword = p.ConfirmPassword;
                    user.Role = "Admin";
                    context.Users.Add(user);
                    context.SaveChanges();
                    return RedirectToAction("Login", "Login");

                }
                else
                {
                    ModelState.AddModelError("", "Sistemde bu mail ile kayıtlı bir kullanıcı bulunmaktadır.");
                }

            }
            else { return View(); }

            return View();
        }
    }
}
