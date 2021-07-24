using System;
using System.Threading.Tasks;

using AppointmentScheduling.Models;
using AppointmentScheduling.Models.ViewModels;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentScheduling.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(ApplicationDbContext db, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var isSignedIn = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            if (!isSignedIn.Succeeded)
            {
                ModelState.AddModelError("", "Invalid Login credentials");
                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            if (!_roleManager.RoleExistsAsync(Helpers.UserTypes.Admin).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(Helpers.UserTypes.Admin)).Wait();
                _roleManager.CreateAsync(new IdentityRole(Helpers.UserTypes.Doctor)).Wait();
                _roleManager.CreateAsync(new IdentityRole(Helpers.UserTypes.Patient)).Wait();
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Name = model.Name
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, model.RoleNme).Wait();
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                foreach(var m in result.Errors)
                {
                    ModelState.AddModelError(m.Code, m.Description);
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            _signInManager.SignOutAsync().Wait();
            return RedirectToAction("Login");
        }
    }
}
