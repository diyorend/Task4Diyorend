using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Task4Diyorend.Data;
using Task4Diyorend.Models;
using Task4Diyorend.ViewModels;

namespace Task4Diyorend.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly DataContext _context;

        public AccountController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            DataContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            RegisterViewModel registerViewModel = new RegisterViewModel();
            return View(registerViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Model State is not valid!";
                return View(registerViewModel);
            }
            var user = await _userManager.FindByEmailAsync(registerViewModel.Email);
            if (user == null)
            {
                var newUser = new AppUser()
                {
                    Email = registerViewModel.Email,
                    UserName = registerViewModel.Email,
                    Name = registerViewModel.Name
                };

                var result = await _userManager.CreateAsync(newUser, registerViewModel.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(newUser, isPersistent: false);
                    TempData["Success"] = "You're registered!";
                    return RedirectToAction("Index", "Home");
                }
                TempData["Error"] = "Something went wrong on creating user!";
                return View(registerViewModel);
            }
            TempData["Error"] = "Email is already in use!";
            return View(registerViewModel);
        }
        //Login
        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Model State is not valid!";
                return View(loginViewModel);
            }
            var user = await _userManager.FindByEmailAsync(loginViewModel.Email);
            if (user != null)
            {
                if (!user.ActiveStatus)
                {
                    TempData["Error"] = "This user is blocked!";
                    return View(loginViewModel);
                }
                var passwordCheck = await _userManager
                    .CheckPasswordAsync(user, loginViewModel.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager
                        .PasswordSignInAsync(user, loginViewModel.Password, false, false);
                    if (result.Succeeded)
                    {
                        //Last Login time
                        user.LastLoginTime = DateTime.Now;
                        _context.Users.Update(user);
                        _context.SaveChanges();

                        TempData["Success"] = "You're logged in!";
                        return RedirectToAction("Index", "Home");
                    }
                }
                TempData["Error"] = "Wrong Password";
                return View(loginViewModel);
            }
            TempData["Error"] = "Email is not registered!";
            return View(loginViewModel);
        }
        //logout
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            TempData["Success"] = "You're logged out!";
            return RedirectToAction("Index", "Home");
        }
    }
}
