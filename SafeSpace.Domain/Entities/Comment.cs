using SafeSpace.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace SafeSpace.Domain.Entities
{
    public class Comment : AuditableEntity
    {
        [Required]
        public Guid UserId { get; set; }

        public User User { get; set; } = null!;

        [Required]
        public Guid StoryId { get; set; }

        public Story Story { get; set; } = null!;

        [Required(ErrorMessage = "Comment cannot be empty.")]
        public string Text { get; set; } = string.Empty;

        [Required]
        public int likeAmount { get; set; } = 0;
    }
}
