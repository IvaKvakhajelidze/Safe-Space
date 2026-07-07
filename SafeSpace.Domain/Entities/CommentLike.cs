using SafeSpace.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
namespace SafeSpace.Domain.Entities
{
    public class CommentLike : AuditableEntity
    {
        public Guid CommentId { get; set; }
        public Guid UserId { get; set; }

        public Comment Comment { get; set; }
        public User User { get; set; }
    }
}
