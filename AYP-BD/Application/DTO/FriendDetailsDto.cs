using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class FriendDetailsDto
    {
        public string SteamId { get; set; }
        [JsonProperty("profilestate")]
        public bool IsOnline { get; set; }
        [JsonProperty("personaname")]
        public string NickName { get; set; }
        [JsonProperty("profileurl")]
        public string ProfileUrl { get; set; }
        [JsonProperty("avatarfull")]
        public string AvatarFull { get; set; }
        public string RealName { get; set; }
        public string TimeCreated { get; set; }
        [JsonProperty("loccountrycode")]
        public string Loccountrycode { get; set; }
    }
}
