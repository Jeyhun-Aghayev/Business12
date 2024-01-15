using Business.Helpers;
using Business.Models;
using Business.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Business.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
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
        public async Task<IActionResult> Register(RegisterVm vm)
        {
            if(!ModelState.IsValid) return View(vm);
            AppUser us = await _userManager.FindByEmailAsync(vm.UsernameOrEmail);
            AppUser user = new AppUser()
            {
                Name = vm.Name,
                Surname = vm.Surname,
                UserName = vm.UserName,
                Email = vm.Email,
            };
            var create = await _userManager.CreateAsync(user,vm.Password);
            await _userManager.AddToRoleAsync(user, UserRole.Member.ToString());
            if (!create.Succeeded)
            {
                foreach (var item in create.Errors)
                {
                    ModelState.AddModelError(String.Empty, item.Description);
                }
                return View(vm);
            }
            return RedirectToAction(nameof(Index),"Home");
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVm vm,string? returnUrl)
        {
            if(!ModelState.IsValid) return View(vm);
            AppUser user = (await _userManager.FindByNameAsync(vm.UsernameOrEmail) )??( await _userManager.FindByEmailAsync(vm.UsernameOrEmail));
            if (user == null) ModelState.AddModelError("UsernameOrEmail", "Username/Emnail or Password incorrect");
            var result = _signInManager.CheckPasswordSignInAsync(user, vm.Password,true).Result;
            if(result.IsLockedOut)  ModelState.AddModelError(String.Empty, "Try again later"); 
            if(!result.Succeeded) ModelState.AddModelError("UsernameOrEmail", "Username/Emnail or Password incorrect");
            await _signInManager.SignInAsync(user, vm.RememberMe);
            return (returnUrl is not null && (returnUrl.Contains("Login") || returnUrl.Contains("Register")) ? RedirectToAction(nameof(Index), "Home") : (ActionResult)Redirect(returnUrl));
        }
        public IActionResult Logout() { _signInManager.SignOutAsync(); return RedirectToAction(nameof(Index), "Home"); }
        public async Task<IActionResult> CreateRole()
        {
            foreach (var item in Enum.GetValues(typeof(UserRole)))
            {
                if (await _roleManager.FindByNameAsync(item.ToString()) == null)
                {
                    await _roleManager.CreateAsync(new IdentityRole()
                    {
                        Name = item.ToString()
                    });
                };
            };
            return RedirectToAction(nameof(Index), "Home");
        }
    }
}
