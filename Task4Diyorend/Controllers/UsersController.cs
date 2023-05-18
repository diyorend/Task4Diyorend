using Microsoft.AspNetCore.Mvc;
using Task4Diyorend.Data;
using Task4Diyorend.Models;
using Task4Diyorend.Repository.IRepository;

namespace Task4Diyorend.Controllers
{
    public class UsersController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;
        private readonly DataContext _context;

        public UsersController(
            IHttpContextAccessor httpContextAccessor,
            IUserRepository userRepository,
            DataContext context) 
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return View(users);
        }
        [HttpGet]
        public async Task<IActionResult> BlockToggle(string id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            var currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            
            if(user == null)
            {
                TempData["error"] = "User not found!";
                return View();
            }
            user.ActiveStatus = !user.ActiveStatus;
            _userRepository.Update(user);
            if (!user.ActiveStatus && user.Id == currentUserId)
            {
                return RedirectToAction("Logout", "Account");
            }
            return RedirectToAction("Index", "Users");
            
        }
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            var currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();

            if (user == null)
            {
                TempData["error"] = "User not found!";
                return View();
            }
            
            _userRepository.Delete(user);
            if(user.Id == currentUserId)
            {
                return RedirectToAction("Logout", "Account");
            }
            return RedirectToAction("Index", "Users");

        }
    }
}
