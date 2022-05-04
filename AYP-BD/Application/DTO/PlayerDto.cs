using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class PlayerDto
    {
        [JsonProperty("personaname")]
        public string SteamNickName { get; set; }
        [JsonProperty("profileurl")]
        public string ProfileUrl { get; set; }
        [JsonProperty("avatarfull")]
        public string AvatarfullUrl { get; set; }
        [JsonProperty("realname")]
        public string RealName { get; set; }
        [JsonProperty("timecreated")]
        public string? AccountCreated { get; set; }
        [JsonProperty("loccountrycode")]
        public string SteamNationality { get; set; }
        [JsonProperty("steamid")]
        public string SteamId { get; set; }
        [JsonProperty("personastate")]
        public bool IsOnline { get; set; }
        public PlayerDto()
        {

        }
    }
}
