using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class FriendListDto
    {
        [JsonProperty("friendslist")]
        public FriendsList FriendsList { get; set; }
    }

    public class FriendsList
    {
        [JsonProperty("friends")]
        public List<Friend> Friends { get; set; }
    }
 
    public class Friend
    {
        public string SteamId { get; set; }
        public string Relationship { get; set; }
        [JsonProperty("friend_since")]
        public long FriendSince { get; set; }
    }
}
