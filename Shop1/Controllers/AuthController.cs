using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop1.Data;
using Shop1.Models;
using Shop1.Models.ViewModel.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Shop1.Controllers
{
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost("login")]
        public IActionResult Login(LoginDTO login)
        {
            var user = _context.Users.FirstOrDefault(x => x.Email == login.Email && login.Password == login.Password);
            if (user == null)
            {
                return View();
            }
            var userClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Email,user.Email),
                    new Claim(ClaimTypes.Name,user.Email),
                    new Claim(ClaimTypes.Role,user.Role),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())

                };
            var userIdentity = new ClaimsIdentity(userClaims, "User Identity");

            var userPrincipal = new ClaimsPrincipal(new[] { userIdentity });
            HttpContext.SignInAsync(userPrincipal);
            return RedirectToAction("index", "home");
        }

        [HttpGet("register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterDTO registerDTO)
        {
            var user = new User { Email = registerDTO.Email, Password = registerDTO.Password, Role = "user" };
            _context.Users.Add(user);
            var check = _context.SaveChanges();
            if (check > 0)
            {
                return Login(new LoginDTO { Email = registerDTO.Email, Password = registerDTO.Password });
            }
            return View();
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("index", "home");
        }
    }
}
