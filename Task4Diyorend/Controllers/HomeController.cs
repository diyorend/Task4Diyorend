using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Task4Diyorend.Models;
using Task4Diyorend.Repository.IRepository;

namespace Task4Diyorend.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;

        public HomeController(ILogger<HomeController> logger,
            IHttpContextAccessor httpContextAccessor,
            IUserRepository userRepository)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }

        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();
                AppUser currentUser = await _userRepository.GetByIdAsync(currentUserId);
                return View(currentUser);
            }
            return View();
            
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}