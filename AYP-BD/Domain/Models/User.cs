using Domain.Common;

namespace Domain.Models
{
    public class User : AuditableEntity, IEntity
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string NickName { get; set; }
        public string SteamNickName { get; set; }
        public int? PhoneNumber { get; set; }
        public string Nationality { get; set; }
        public string PasswordHash { get; set; }
        public virtual Role Role { get; set; }
        public byte RoleId { get; set; }
        public bool? IsBanned { get; set; }
        public bool? IsActive { get; set; }
        public string Gender { get; set; }
        public DateTime? LastLogOn { get; set; }
        #region domain actions
        public User Update()
        {

            return this;
        }
        #endregion
    }
}
