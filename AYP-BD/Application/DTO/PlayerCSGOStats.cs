using Newtonsoft.Json;

namespace Application.DTO
{
    public class PlayerCSGOStats
    {
        public PlayerStats PlayerStats { get; set; }
    }
    public class PlayerStats
    {
        [JsonProperty("steamID")]
        public string SteamId { get; set; }
        public List<Stats> Stats { get; set; }
    }
    public class Stats
    {
        public string Name { get; set; }
        public long Value { get; set; }
    }
}
