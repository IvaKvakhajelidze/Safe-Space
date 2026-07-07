using SafeSpace.Domain.Entities;

namespace SafeSpace.MVC.Models
{
    public class HomeViewModel
    {
        public IEnumerable<Story> Stories { get; set; } = new List<Story>();

        public ZenQuoteResponse? Quote { get; set; }
    }
}