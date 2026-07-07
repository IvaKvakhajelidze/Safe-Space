using Microsoft.AspNetCore.Mvc;
using SafeSpace.Application.Services;
using SafeSpace.MVC.Models;
using System.Diagnostics;
using SafeSpace.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;
using SafeSpace.Domain.Constants;
namespace SafeSpace.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStoryService _storyService;
        private readonly IQuoteService _quoteService;
        private readonly IMemoryCache _cache;

        public HomeController(ILogger<HomeController> logger, IStoryService storyService, IMemoryCache cache, IQuoteService quoteService)
        {
            _logger = logger;
            _storyService = storyService;
            _cache = cache;
            _quoteService = quoteService;
        }


        public async Task<IActionResult> Index(Guid? storyId = null, int commentsToShow = 2)
        {
            ViewBag.ScrollToStoryId = storyId;

            ZenQuoteResponse? quoteResponse = null;

            try
            {
                if (!_cache.TryGetValue("CurrentQuote", out quoteResponse))
                {
                    quoteResponse = await _quoteService.GetRandomQuoteAsync();

                    _cache.Set("CurrentQuote", quoteResponse,  new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(1)));
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Could not retrieve quote from ZenQuotes.");

                quoteResponse = InspirationalQuotes.Quotes[ Random.Shared.Next(InspirationalQuotes.Quotes.Count)];
            }

            var stories = await _storyService.GetStoriesWithCommentsAsync();

            var model = new HomeViewModel
            {
                Stories = stories,
                Quote = quoteResponse
            };

            return View(model);
        }

        public async Task<IActionResult> Privacy()
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
