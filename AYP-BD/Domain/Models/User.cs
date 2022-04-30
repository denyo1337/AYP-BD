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
        public string CommunityUrl { get; set; }
        public byte[] Avatar { get; set; }
        public DateTime? LastLogOn { get; set; }
        public long? SteamId { get; set; }
        public virtual SteamUserData SteamUserData { get; set; }
        public long? SteamUserDataId { get; set; }

        #region domain actions
        public User Update(string email, string nickname, int? phoneNumber, string nationality, string steamNickName)
        {
            if(!string.IsNullOrEmpty(email))
                Email = email;
            if (!string.IsNullOrEmpty(nickname))
                NickName = nickname;
            if(!string.IsNullOrEmpty(steamNickName))
                SteamNickName = steamNickName;

            PhoneNumber = phoneNumber;
            Nationality = nationality;

            return this;
        }

        public User UpdateSteamId(long? steamId, bool? resetValue = false)
        {
            if (SteamId != steamId && steamId.HasValue)
            {
                SteamId = steamId;
                LastModified = DateTime.Now;
                LastModifiedBy = Email;
            }
            if (resetValue.HasValue && resetValue.Value)
            {
                SteamId = null;
                LastModified = DateTime.Now;
                LastModifiedBy = Email;
            }
            return this;
        }
        #endregion
    }
}
