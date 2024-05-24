using Core.Models;
using Exsam.DTOs.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Exsam.Controllers
{
    public class AccountController : Controller
    {
        UserManager<AppUser> _userManager;
        SignInManager<AppUser> _signInManager;
        RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Register()
        {
            return View();
        }

        //public async Task<IActionResult> CreateRoles()
        //{
        //    IdentityRole role1 = new IdentityRole("Admin");
        //    IdentityRole role2 = new IdentityRole("Member");
        //    await _roleManager.CreateAsync(role2);
        //   var result = await _roleManager.CreateAsync(role1);
        //    return Ok(result);
        //}

        //public async Task<IActionResult> CreateAdmin()
        //{
        //    AppUser user = new AppUser()
        //    {
        //        FullName = "Admin Admin",
        //        Email = "Admin@gmail.com",
        //        UserName = "Admin"
        //    };
        //    await _userManager.CreateAsync(user, "Admin123@");

        //    await _userManager.AddToRoleAsync(user, "Admin");
        //    return Ok("Ok");
        //}

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto register)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            AppUser user = new AppUser()
            {
                FullName = register.FullName,
                Email = register.Email,
                UserName = register.UserName
            };
            var result = await _userManager.CreateAsync(user, register.Password);
            if (!result.Succeeded)
            {
                foreach(var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View();
            }
            await _userManager.AddToRoleAsync(user, "Member");
            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto login)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            AppUser user = await _userManager.FindByNameAsync(login.UserName);
            if(user == null)
            {
                ModelState.AddModelError("", "UserName Or Password is nt valid!");
                return View();
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "UserName Or Password is nt valid!");
                return View();
            }

            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "You are Banned! Try it later");
                return View();
            }

            await _signInManager.PasswordSignInAsync(user, login.Password,login.RememberMe,false);
            return RedirectToAction("Index", "Home");
        }


        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
