using Microsoft.AspNetCore.Mvc;
using SafeSpace.Application.Services;
using SafeSpace.Domain.Entities;
using SafeSpace.MVC.Models;

namespace SafeSpace.MVC.Controllers
{
    public class ProfileController : Controller
    {
        private ILogger<ProfileController> _logger;
        private readonly IUserService _userService;
        private readonly IStoryService _storyService;
        private readonly IProfanityFilterService _profanityFilterService;

        public ProfileController(ILogger<ProfileController> logger, IUserService userService, IStoryService storyService, IProfanityFilterService profanityFilterService)
        {
            _logger = logger;
            _userService = userService;
            _storyService = storyService;   
            _profanityFilterService = profanityFilterService;
        }

        public async Task<IActionResult> Index()
        {
            var userIdStr = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userIdStr))
            {
                return RedirectToAction("SignIn", "Account");
            }

            if (!Guid.TryParse(userIdStr, out Guid userId))
            {
                return RedirectToAction("SignIn", "Account");
            }

            var user = await _userService.GetByIdAsync(userId);

            if (user == null)
            {
                return RedirectToAction("SignIn", "Account");
            }

            var stories = await _storyService.GetStoriesByUserAsync(userId);

            var model = new ProfileViewModel
            {
                User = user,
                Stories = stories
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            string? userId =  HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("SignIn", "Account");
            }

            var user = await _userService.GetByIdAsync(Guid.Parse(userId));

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(User user)
        {
            ModelState.Remove("PasswordHash");
            ModelState.Remove("Stories");
            ModelState.Remove("Comments");

            user.Name = user.Name?.Trim();
            user.Email = user.Email?.Trim();

            if (!ModelState.IsValid)
            {
                return View(user);
            }

            var existingUser = await _userService.GetByIdAsync(user.Id);

            if (existingUser == null)
            {
                return NotFound();
            }

            existingUser.Name = await _profanityFilterService.CleanTextAsync(existingUser.Name);
            existingUser.Email = user.Email;
            existingUser.DateOfBirth = user.DateOfBirth;
            existingUser.UpdatedAt = DateTime.Now;

            await _userService.UpdateAsync(existingUser);

            return RedirectToAction("Index", "Profile");
        }
        

        [HttpGet]
        public async Task<IActionResult> Delete()
        {
            string? userId = HttpContext.Session.GetString("UserId");

            var user = await _userService.GetByIdAsync(Guid.Parse(userId));

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed()
        {
            string? userId = HttpContext.Session.GetString("UserId");

            await _userService.DeleteAccountAsync(Guid.Parse(userId));

            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
