namespace SafeSpace.Domain.Common
{
    public abstract class SoftDeleteEnabledEntity : AuditableEntity
    {
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
