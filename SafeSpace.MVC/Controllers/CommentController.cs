using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SafeSpace.Application.Services;
using SafeSpace.Domain.Entities;

namespace SafeSpace.MVC.Controllers
{
    public class CommentController : Controller
    {
        private ILogger<CommentController> _logger;
        private readonly ICommentService _commentService;
        private readonly ICommentLikeService _commentLikeService;
        private readonly IProfanityFilterService _profanityFilterService;

        public CommentController(ILogger<CommentController> logger, ICommentService commentService, ICommentLikeService commentLikeService, IProfanityFilterService profanityFilterService)
        {
            _logger = logger;
            _commentService = commentService;
            _commentLikeService = commentLikeService;
            _profanityFilterService = profanityFilterService;
        }

        public async Task<IActionResult> Create(Comment comment, Guid storyId)
        {
            try
            {
                comment.Text = comment.Text?.Trim();

                ModelState.Clear();

                if (string.IsNullOrWhiteSpace(comment.Text))
                {
                    ModelState.AddModelError("Text", "Text cannot be empty.");
                }

                if (!ModelState.IsValid)
                {
                    TempData["CommentError"] = "Comment cannot be empty.";
                    TempData["CommentErrorStoryId"] = comment.StoryId;

                    return RedirectToAction("Index", "Home", new { storyId });
                }

                string userId = HttpContext.Session.GetString("UserId");

                if (userId.IsNullOrEmpty())
                {
                    return RedirectToAction("Register", "Account");
                }

                comment.UserId = Guid.Parse(userId);

                bool containsProfanity = await _profanityFilterService.ContainsProfanityAsync(comment.Text);
                if (containsProfanity)
                {
                    TempData["CommentError"] = "Your comment contains inappropriate language.";
                    TempData["CommentErrorStoryId"] = comment.StoryId;

                    return RedirectToAction("Index", "Home", new { storyId });
                }

                await _commentService.CreateAsync(comment);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Commenting failed");

                TempData["CommentError"] =  "Something went wrong while posting your comment.";
                TempData["CommentErrorStoryId"] = comment.StoryId;

                return RedirectToAction("Index", "Home", new { storyId });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Like(Guid commentId, Guid storyId)
        {
            var userId = HttpContext.Session.GetString("UserId");

            if (userId.IsNullOrEmpty())
            {
                return RedirectToAction("Register", "Account");
            }

            var parsedUserId = Guid.Parse(userId);

            await _commentLikeService.AddLikeAsync(commentId, parsedUserId);

            return RedirectToAction("Index", "Home", new { storyId });
        }
    }
}
