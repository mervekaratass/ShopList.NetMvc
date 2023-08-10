using EntityLayer.Entities;
using EntityLayer.Context;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using ShoppingList.ViewModels;
using System.Diagnostics.Eventing.Reader;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using NuGet.Common;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace ShoppingList.Controllers
{
    public class LoginController : Controller
    {
        ShoppingDbContext _context;


        public LoginController()
        {
           _context = new ShoppingDbContext();
        }

        
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            User user = _context.Users.Where(x => x.Email == model.Email && x.Password == model.Password).SingleOrDefault();

            if (ModelState.IsValid)
            {

                if (user != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name,user.UserName),
                        new Claim(ClaimTypes.Surname,user.UserSurname),
                        new Claim(ClaimTypes.Email,user.Email),
                        new Claim(ClaimTypes.Role,user.Role)

                    };

                    var claimsIdentity=new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties=new AuthenticationProperties();
                   
                     await  HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,new ClaimsPrincipal(claimsIdentity),
                         authProperties);
                    
                    return RedirectToAction("List", "User", new { id = user.UserID });
                }

                ViewData["Login"] = "false";
                return View();
            }

            else
            {
                return View();
            }

        }


        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Login");
        }

    }

}

