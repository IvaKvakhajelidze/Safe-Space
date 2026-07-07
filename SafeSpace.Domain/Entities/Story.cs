using SafeSpace.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace SafeSpace.Domain.Entities
{
    public class Story : SoftDeleteEnabledEntity
    {
        [Required]
        public Guid UserId { get; set; }

        public User User { get; set; } = null!;

        [Required(ErrorMessage = "Title cannot be empty.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Text cannot be empty.")]
        public string Text { get; set; } = string.Empty;

        public List<Comment> Comments { get; set; } = new();

    }
}
