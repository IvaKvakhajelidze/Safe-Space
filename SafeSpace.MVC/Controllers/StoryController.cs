using Microsoft.AspNetCore.Mvc;
using SafeSpace.Application.Services;
using SafeSpace.Domain.Entities;

namespace SafeSpace.MVC.Controllers
{
    public class StoryController : Controller
    {
        public ILogger<StoryController> _logger;
        private readonly IStoryService _storyService;
        private readonly IProfanityFilterService _profanityFilterService;

        public StoryController(ILogger<StoryController> logger, IStoryService storyService, IProfanityFilterService profanityFilterService)
        {
            _logger = logger;
            _storyService = storyService;
            _profanityFilterService = profanityFilterService;
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Story story)
        {
            try
            {
                story.Title = story.Title?.Trim();
                story.Text = story.Text?.Trim();

                ModelState.Remove("Title");
                ModelState.Remove("Text");
                ModelState.Remove("User");

                if (!ModelState.IsValid)
                {
                    return View(story);
                }

                string? userId = HttpContext.Session.GetString("UserId");
                if (string.IsNullOrEmpty(userId))
                {
                    return RedirectToAction("Create", "Register");
                }


                story.UserId = Guid.Parse(userId);
                story.Title = await _profanityFilterService.CleanTextAsync(story.Title);
                story.Text = await _profanityFilterService.CleanTextAsync(story.Text);

                await _storyService.CreateAsync(story);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Posting a story failed");
                return View(story);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid storyId)
        {
            string? userId =HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Index", "SignIn");
            }

            var story = await _storyService.GetByIdAsync(storyId);

            if (story == null)
            {
                return NotFound();
            }

            if (story.UserId != Guid.Parse(userId))
            {
                return Unauthorized();
            }

            story.IsDeleted = true;
            story.DeletedAt = DateTime.UtcNow;
            await _storyService.UpdateAsync(story);

            return RedirectToAction("Index", "Profile");
        }
    }
}
