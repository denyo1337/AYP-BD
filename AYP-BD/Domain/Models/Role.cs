using Newtonsoft.Json;

namespace Domain.Models
{
    public class Role
    {
        public byte Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
