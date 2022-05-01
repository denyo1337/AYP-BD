using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class SteamUserData : IEntity
    {
        public long Id { get; set; }
        public string PersonName { get; set; }
        public string ProfileUrl { get; set; }
        public string AvatarfullUrl { get; set; }
        public string RealName { get; set; }
        public DateTime? AccountCreated { get; set; }
        public string SteamNationality { get; set; }
        public User User { get; set; }
        public long? UserId { get; set; }
        #region SteamUserData domain actions
        public SteamUserData()
        {

        }
        public SteamUserData(long id, string steamNickname, string profileUrl, string avatarUrl, string realName, DateTime accountCreated, string nationality)
        {
            Id = id;
            PersonName = steamNickname;
            ProfileUrl = profileUrl;
            AvatarfullUrl = avatarUrl;
            RealName = realName;
            AccountCreated = accountCreated;
            SteamNationality = nationality;
        }
        public SteamUserData UpdateSteamUserData(string steamNickname, string profileUrl, string avatarUrl, string realName, DateTime accountCreated, string nationality)
        {
            if (PersonName != steamNickname)
                PersonName = steamNickname;
            if (ProfileUrl != profileUrl)
                ProfileUrl = profileUrl;
            if (AvatarfullUrl != avatarUrl)
                AvatarfullUrl = avatarUrl;
            if (RealName != realName)
                RealName = realName;
            if (AccountCreated != accountCreated)
                AccountCreated = accountCreated;
            if (SteamNationality != nationality)
                SteamNationality = nationality;
            return this;
        }
        #endregion
    }

}
