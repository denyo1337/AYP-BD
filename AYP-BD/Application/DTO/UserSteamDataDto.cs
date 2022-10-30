using Newtonsoft.Json;

namespace Application.DTO
{
    public class UserSteamDataDto
    {
        public Response Response { get; set; }
    }
    public class Response
    {
        public List<Player> Players { get; set; }
    }
    public class Player
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
        public long AccountCreated { get; set; }
        [JsonProperty("loccountrycode")]
        public string SteamNationality { get; set; }
        [JsonProperty("steamid")]
        public string SteamId { get; set; }
        [JsonProperty("personastate")]
        public int IsOnline { get; set; }
        [JsonProperty("communityvisibilitystate")]
        public int Communityvisibilitystate { get; set; }

        public Player()
        {

        }
    }
}
