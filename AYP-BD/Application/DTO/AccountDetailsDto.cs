using Domain.Models;
using System.Globalization;

namespace Application.DTO
{
    public class AccountDetailsDto
    {
        public string Email { get; set; }
        public string NickName { get; set; }
        public string SteamNickName { get; set; }
        public int? PhoneNumber { get; set; }
        public string Nationality { get; set; }
        public string Gender { get; set; }
        public string LastLogOn { get; set; }
        public string ProfileUrl { get; set; }
        public string AvatarUrl { get; set; }
        public string RealName { get; set; }
        public string SteamAccountCreatedAt { get; set; }
        public string SteamId { get; set; }

        public AccountDetailsDto(User user)
        {
            Email = user.Email;
            NickName = user.NickName;
            SteamNickName = user.SteamUserData?.PersonName;
            PhoneNumber = user.PhoneNumber;
            Nationality = user.Nationality ?? user.SteamUserData.SteamNationality;
            Gender = user.Gender;
            ProfileUrl = user.SteamUserData?.ProfileUrl;
            AvatarUrl = user.SteamUserData?.AvatarfullUrl;
            RealName = user.SteamUserData?.RealName;
            SteamAccountCreatedAt = user.SteamUserData?.AccountCreated?.ToString("g", CultureInfo.GetCultureInfo("de-DE"));
            SteamId = user.SteamId.ToString();
            LastLogOn = user.LastLogOn.Value.ToString("g", CultureInfo.GetCultureInfo("de-DE"));
        }
    }
}
