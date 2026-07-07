using SafeSpace.Domain.Common;
using System.ComponentModel.DataAnnotations;
using SafeSpace.Domain.Validation;

namespace SafeSpace.Domain.Entities
{
    public class User : SoftDeleteEnabledEntity
    {
        [Required(ErrorMessage = "Name cannot be empty.")]
        [MaxLength(50, ErrorMessage = "Name can't exceed 50 characters!")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Date Of Birth cannot be empty.")]
        [MinimumAge(13)]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Password cannot be empty.")]
        [MaxLength(16, ErrorMessage = "Name can't exceed 50 characters!")]
        public string PasswordHash { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email cannot be empty.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string? Email { get; set; }


        public List<Story> Stories { get; set; } = new();
        public List<Comment> Comments { get; set; } = new();
    }
}
