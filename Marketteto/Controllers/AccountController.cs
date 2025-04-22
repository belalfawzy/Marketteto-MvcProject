using Marketteto.Data.StaticMember;
using Marketteto.Data.ViewModel;
using Marketteto.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace Marketteto.Controllers
{
    public class AccountController : Controller
    {
        private readonly MarkettetoDbContext _context;
        private readonly UserManager<User> _user;
        private readonly SignInManager<User> _signInManager;
        public AccountController(MarkettetoDbContext context, UserManager<User> user, SignInManager<User> signInManager)
        {
            _context = context;
            _user = user;
            _signInManager = signInManager;
        }
        public IActionResult Login()
        {
            var res = new LoginViewModel();
            return View(res);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel lm)
        {
            if (!ModelState.IsValid)
                return View(lm);

            var user = await _user.FindByEmailAsync(lm.EmailAddress.Trim().ToLower());
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, lm.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Product");
                }
            }

            ModelState.AddModelError("", "Invalid email or password");
            return View(lm);
        }
        public IActionResult Register()
        {
            var res = new RegisterViewModel();
            return View(res);
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel rVM)
        {
            var user = await _user.FindByEmailAsync(rVM.EmailAddress);
            if(user != null)
            {
                return View(rVM);
            }
            var newUser = new User()
            {
                Email = rVM.EmailAddress,
                UserName = rVM.EmailAddress.Split('@')[0],
                FullName = rVM.FullName
            };
            var res = await _user.CreateAsync(newUser,rVM.Password);
            if (res.Succeeded)
            {
                await _user.AddToRoleAsync(newUser, UserRoles.User);
            }
            return View("CompleteRegister");
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Product");
        }
        public async Task<IActionResult> Users()
        {
            var res = await _context.Users.ToListAsync();
            return View(res);
        }
    }
}
