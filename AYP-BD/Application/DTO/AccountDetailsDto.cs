using Domain.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public AccountDetailsDto(User user)
        {
            Email = user.Email;
            NickName = user.NickName;
            SteamNickName = user.SteamNickName;
            PhoneNumber = user.PhoneNumber;
            Nationality = user.Nationality;
            Gender = user.Gender;
            LastLogOn = user.LastLogOn.Value.ToString("g", CultureInfo.GetCultureInfo("de-DE"));
        }
    }
}
