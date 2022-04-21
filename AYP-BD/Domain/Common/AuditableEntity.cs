using Domain.Common;

namespace Domain.Common
{
    public abstract class AuditableEntity
    {
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; } = "self";
        public DateTime? LastModified { get; set; }
        public string LastModifiedBy { get; set; }
    }
}
