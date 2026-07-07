using SafeSpace.Domain.Entities;

namespace SafeSpace.MVC.Models
{
    public class ProfileViewModel
    {
        public User User { get; set; }

        public IEnumerable<Story> Stories { get; set; } = new List<Story>();
    }
}
