namespace SafeSpace.Domain.Common
{
    public abstract class AuditableEntity : BaseEntity
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
    }
}
