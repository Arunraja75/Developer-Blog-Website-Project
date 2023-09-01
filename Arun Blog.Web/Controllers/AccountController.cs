using ArunBlog.Web.Models.Domain.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ArunBlog.Web.Models.ViewModels;

namespace ArunBlog.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;

        public SignInManager<IdentityUser> SignInManager { get; }

        public AccountController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            SignInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var identityUser = new IdentityUser
                {
                    UserName = registerViewModel.UserName,
                    Email = registerViewModel.Email
                };

                var identityResult = await userManager.CreateAsync(identityUser, registerViewModel.Password);

                if (identityResult.Succeeded)
                {
                    //assign this user the "user" role 
                    var roleIdentityResult = await userManager.AddToRoleAsync(identityUser, "User");

                    if (roleIdentityResult.Succeeded)
                    {
                        //success notification
                        return RedirectToAction("Register");
                    }
                }
            }
            return View();
        }
        [HttpGet]     
        public IActionResult Login(string ReturnUrl)
        {
            var model = new LoginViewModel
            {
                ReturnUrl = ReturnUrl
            };
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var signInResult = await SignInManager.PasswordSignInAsync(loginViewModel.UserName,
                loginViewModel.Password, false, false);

            if (signInResult != null && signInResult.Succeeded)
            {
                if (string.IsNullOrWhiteSpace(loginViewModel.ReturnUrl))
                {
                    return RedirectToPage(loginViewModel.ReturnUrl);
                }
                
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await SignInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
