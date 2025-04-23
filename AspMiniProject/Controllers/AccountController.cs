using AspMiniProject.Helpers.Enums;
using AspMiniProject.Models;
using AspMiniProject.ViewModels.UI.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.Data;

namespace AspMiniProject.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<AppUser> userManager,
                                SignInManager<AppUser> signInManager,
                                RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            AppUser user = new()
            {
                FullName = request.FullName,
                Email = request.Email,
                UserName = request.UserName,
            };

            var res = await _userManager.CreateAsync(user, request.Password);

            if (!res.Succeeded)
            {
                foreach (var item in res.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View(request);
            }
            await _userManager.AddToRoleAsync(user, Roles.Member.ToString());

            await _signInManager.SignInAsync(user, false);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            AppUser user = await _userManager.FindByEmailAsync(request.EmailOrUserName);
            if (user is null)
            {
                user = await _userManager.FindByNameAsync(request.EmailOrUserName);
            }
            if (user is null)
            {
                ModelState.AddModelError("", "Email, username or password is wrong.");
                return View(request);
            }

            var res = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!res)
            {
                ModelState.AddModelError("", "Email, username or password is wrong.");
                return View(request);
            }

            var response = await _signInManager.PasswordSignInAsync(user, request.Password, false, false);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        //[HttpGet]
        //public async Task<ActionResult> CreateRoles()
        //{
        //    foreach (var role in Enum.GetValues(typeof(Roles)))
        //    {
        //        if (!await _roleManager.RoleExistsAsync(role.ToString()))
        //        {
        //            await _roleManager.CreateAsync(new IdentityRole { Name = role.ToString() });
        //        }
        //    }
        //    return Ok();
        //}
    }
}
