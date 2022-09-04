using Domain.Common;

namespace Domain.Models
{
    public class User : AuditableEntity, IEntity
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string NickName { get; set; }
        public int? PhoneNumber { get; set; }
        public string Nationality { get; set; }
        public string PasswordHash { get; set; }
        public virtual Role Role { get; set; }
        public byte RoleId { get; set; }
        public bool? IsBanned { get; set; }
        public bool? IsActive { get; set; }
        public string Gender { get; set; }
        public string CommunityUrl { get; set; }
        public DateTime? LastLogOn { get; set; }
        public long? SteamId { get; set; }
        public DateTime? LastSteamIdUpdate { get; set; }
        public virtual SteamUserData SteamUserData { get; set; }
        #region domain actions

        public User Update(string email, string nickname, int? phoneNumber, string nationality)
        {
            if (!string.IsNullOrEmpty(email))
                Email = email;
            if (!string.IsNullOrEmpty(nickname))
                NickName = nickname;


            PhoneNumber = phoneNumber;
            Nationality = nationality;

            return this;
        }

        public User UpdateSteamId(long? steamId, bool? resetValue = false)
        {
            if (SteamId != steamId && steamId.HasValue)
            {
                SteamId = steamId;
                LastModified = DateTime.Now.Date;
                LastModifiedBy = Email;
                LastSteamIdUpdate = DateTime.Now.Date;
            }
            if (resetValue.HasValue && resetValue.Value)
            {
                SteamId = null;
                LastModified = DateTime.Now;
                LastModifiedBy = Email;
                LastSteamIdUpdate = DateTime.Now.Date;
            }
            return this;
        }
        #endregion
    }
}
