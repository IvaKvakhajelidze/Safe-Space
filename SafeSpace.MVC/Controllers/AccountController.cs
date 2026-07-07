using Microsoft.AspNetCore.Mvc;
using SafeSpace.Application.Services;
using SafeSpace.Domain.Entities;
using SafeSpace.Domain.Exceptions;
using SafeSpace.MVC.Models;

namespace SafeSpace.MVC.Controllers
{
    public class AccountController : Controller
    {
        private ILogger<AccountController> _logger;
        private readonly IUserService _userService;
        private readonly IProfanityFilterService _profanityFilterService;

        public AccountController(ILogger<AccountController> logger, IUserService userService, IProfanityFilterService profanityFilterService)
        {
            _logger = logger;
            _userService = userService;
            _profanityFilterService = profanityFilterService;
        }


        [HttpGet]
        public IActionResult SignIn()
        {
            return View(new SignInViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(string email, string password)
        {
            try
            {
                email = email?.Trim();
                password = password?.Trim();

                if (!ModelState.IsValid)
                {
                    return View();
                }

                var user = await _userService.LoginAsync(email, password);

                if (user == null)
                {
                    ModelState.AddModelError("", "Invalid email or password.");
                    return View();
                }

                HttpContext.Session.SetString("UserId", user.Id.ToString());
                HttpContext.Session.SetString("UserName", user.Name);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Sign in failed.");
                ModelState.AddModelError("", "Something went wrong.");
                return View();
            }
        }


        [HttpGet]
        public IActionResult Register()
        {
            var user = new User
            {
                DateOfBirth = DateTime.Today.AddYears(-13)
            };

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User user)
        {
            try
            {
                user.Name = user.Name?.Trim();
                user.Email = user.Email?.Trim();
                user.PasswordHash = user.PasswordHash?.Trim();

                if (!ModelState.IsValid)
                {
                    return View(user);
                }


                user.Name = await _profanityFilterService.CleanTextAsync(user.Name);
                

                await _userService.RegisterAsync(user);

                HttpContext.Session.SetString("UserId", user.Id.ToString());
                HttpContext.Session.SetString("UserName", user.Name);

                return RedirectToAction("Index", "Home");
            }
            catch (CanNotUseTheSameEmailTwiceException ex)
            {
                ModelState.AddModelError("Email", ex.Message);

                return View(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Registration failed");
                return View(user);
            }
        }
    }
}
